namespace NinjaSubs.Web.Areas.Blog.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using NinjaSubs.Data.Models;
    using NinjaSubs.Data.Models.Enums;
    using NinjaSubs.Services;
    using NinjaSubs.Services.Blog;
    using NinjaSubs.Services.Html;
    using NinjaSubs.Web.Areas.Blog.Models.Articles;
    using NinjaSubs.Web.Infrastructure.Extensions;
    using NinjaSubs.Web.Infrastructure.Filters;
    using System;
    using System.Threading.Tasks;

    using static Services.ServiceConstants;
    using static WebConstants;


    [Area(BlogArea)]
    [Authorize(Roles = BlogAuthorRole + "," + AdminRole)]
    public class ArticlesController : Controller
    {
        private readonly IBlogArticleService articleService;
        private readonly ILogService logService;
        private readonly UserManager<User> userManager;
        private readonly IHtmlService htmlService;

        public ArticlesController(IBlogArticleService articleService,
            ILogService logService,
            UserManager<User> userManager,
            IHtmlService htmlService)
        {
            this.articleService = articleService;
            this.logService = logService;
            this.userManager = userManager;
            this.htmlService = htmlService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int page = 1)
        {
            var totalArticles = await this.articleService.TotalAsync();

            if (page > (int)Math.Ceiling((double)totalArticles / BlogArticlesPageSize) || page < 1)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(new ArticleListingViewModel
            {
                Articles = await this.articleService.AllAsync(page),
                TotalArticles = totalArticles,
                CurrentPage = page
            });
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var article = await this.articleService.ById(id);

            if (article == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return this.ViewOrNotFound(article);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Create(PublishOrEditArticleFormModel model)
        {
            model.Content = this.htmlService.Sanitize(model.Content);

            var userId = this.userManager.GetUserId(User);

            await this.articleService.CreateAsync(model.Title, model.Content, userId);

            TempData.AddSuccessMessage($"Successfully created new article {model.Title}.");

            await this.logService.CreateLogAsync(User.Identity.Name, LogType.CreateArticle, model.Title);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var article = await this.articleService.ById(id);

            if (article == null || (User.Identity.Name != article.Author && !User.IsInRole(AdminRole)))
            {
                return RedirectToAction(nameof(Index));
            }

            return View(new PublishOrEditArticleFormModel
            {
                Title = article.Title,
                Content = article.Content
            });
        }

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Edit(int id, PublishOrEditArticleFormModel model)
        {
            var article = await this.articleService.ById(id);

            if (article == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var oldTitle = article.Title;

            await this.articleService.UpdateArticleAsync(id, model.Title, model.Content);

            TempData.AddSuccessMessage($"Successfully edited article {oldTitle}.");

            await this.logService.CreateLogAsync(User.Identity.Name, LogType.EditArticle, model.Title);

            return RedirectToAction(nameof(Details), new { id = id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var article = await this.articleService.ById(id);

            if (article == null || (User.Identity.Name != article.Author && !User.IsInRole(AdminRole)))
            {
                return RedirectToAction(nameof(Index));
            }

            return View(new DeleteArticleViewModel
            {
                Id = id,
                Title = article.Title,
                Content = article.Content
            });
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var article = await this.articleService.ById(id);

            if (article == null || (User.Identity.Name != article.Author && !User.IsInRole(AdminRole)))
            {
                return RedirectToAction(nameof(Index));
            }

            await this.articleService.DeleteArticleAsync(id);

            TempData.AddSuccessMessage($"Successfully deleted article {article.Title}.");

            await this.logService.CreateLogAsync(User.Identity.Name, LogType.DeleteArticle, article.Title);

            return RedirectToAction(nameof(Index));
        }
    }
}
