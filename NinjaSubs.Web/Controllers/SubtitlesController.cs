namespace NinjaSubs.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using NinjaSubs.Data.Models;
    using NinjaSubs.Data.Models.Enums;
    using NinjaSubs.Services;
    using NinjaSubs.Services.Html;
    using NinjaSubs.Services.Subtitles;
    using NinjaSubs.Web.Infrastructure.Extensions;
    using NinjaSubs.Web.Infrastructure.Filters;
    using NinjaSubs.Web.Models.SubtitlesViewModels;
    using System;
    using System.Threading.Tasks;

    using static Services.ServiceConstants;
    using static WebConstants;

    [Authorize(Roles = TranslatorRole + "," + AdminRole)]
    public class SubtitlesController : Controller
    {
        private readonly ISubtitlesService subtitlesService;
        private readonly ILogService logService;
        private readonly UserManager<User> userManager;
        private readonly IHtmlService htmlService;

        public SubtitlesController(ISubtitlesService subtitlesService,
            ILogService logService,
            UserManager<User> userManager,
            IHtmlService htmlService)
        {
            this.subtitlesService = subtitlesService;
            this.logService = logService;
            this.userManager = userManager;
            this.htmlService = htmlService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int page = 1)
        {
            var totalSubtitless = await this.subtitlesService.TotalAsync();

            if (page > (int)Math.Ceiling((double)totalSubtitless / SubtitlesPageSize) || page < 1)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(new SubtitlesListingViewModel
            {
                Subtitles = await this.subtitlesService.AllAsync(page),
                TotalSubtitles = totalSubtitless,
                CurrentPage = page
            });
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var subtitles = await this.subtitlesService.ById(id);

            if (subtitles == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return this.ViewOrNotFound(subtitles);
        }

        public IActionResult AddNew() => View();

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> AddNew(PublishOrEditSubtitlesFormModel model)
        {
            if (!model.File.FileName.EndsWith(".zip"))
            {
                TempData.AddErrorMessage("Your file shoud be a '.zip' file!");
                //ModelState.AddModelError(string.Empty, "Your file shoud be a '.zip' file!");
                return View();
            }

            model.Description = this.htmlService.Sanitize(model.Description);

            var userId = this.userManager.GetUserId(User);

            var fileContents = await model.File.ToByteArrayAsync();

            await this.subtitlesService.CreateAsync(model.Title,
                model.Description, DateTime.UtcNow, model.Language, fileContents, userId, model.Genres);

            TempData.AddSuccessMessage($"Successfully added new subtitles {model.Title}.");

            await this.logService.CreateLogAsync(User.Identity.Name, LogType.AddNewSubtitles, model.Title);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DownloadSubs(int id)
        {
            var subtitles = await this.subtitlesService.ById(id);

            if (subtitles == null)
            {
                return RedirectToAction(nameof(Index));
            }

            await this.subtitlesService.AddDownload(subtitles.Id);

            return File(subtitles.File, "application/zip", subtitles.Title.Replace(' ', '_').Replace('-', '_') + "_.(ninjasubs)" + ".zip");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var subtitles = await this.subtitlesService.ById(id);

            if (subtitles == null || (User.Identity.Name != subtitles.Author && !User.IsInRole(AdminRole)))
            {
                return RedirectToAction(nameof(Index));
            }

            return View(new DeleteSubtitlesViewModel
            {
                Id = id,
                Title = subtitles.Title
            });
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var subtitles = await this.subtitlesService.ById(id);

            if (subtitles == null || (User.Identity.Name != subtitles.Author && !User.IsInRole(AdminRole)))
            {
                return RedirectToAction(nameof(Index));
            }

            await this.subtitlesService.DeleteSubtitlesAsync(id);

            TempData.AddSuccessMessage($"Successfully deleted subs {subtitles.Title}.");

            await this.logService.CreateLogAsync(User.Identity.Name, LogType.DeleteSubtitles, subtitles.Title);

            return RedirectToAction(nameof(Index));
        }
    }
}
