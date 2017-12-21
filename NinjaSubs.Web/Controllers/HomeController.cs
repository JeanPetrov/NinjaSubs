namespace NinjaSubs.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using NinjaSubs.Services.Subtitles;
    using NinjaSubs.Web.Models;
    using NinjaSubs.Web.Models.HomeViewModels;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public class HomeController : Controller
    {
        private readonly ISubtitlesService subtitlesService;

        public HomeController(ISubtitlesService subtitlesService)
        {
            this.subtitlesService = subtitlesService;
        }

        public async Task<IActionResult> Index(HomeIndexViewModel model)
        {
            return View(new HomeIndexViewModel
            {
                LatestSubtitles = await this.subtitlesService.LatestSubs(),
                MostDownloadSubtitles = await this.subtitlesService.Top10DownloadSubs()
            });
        }

        public async Task<IActionResult> Search(SearchFormModel model)
        => View(new SearchViewModel
        {
            SearchText = model.SearchText,
            Subtitles = await this.subtitlesService.FindeAsync(model.SearchText, model.Language),
            Language = model.Language
        });

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
