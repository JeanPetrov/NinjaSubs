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

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Search(SearchFormModel model)
        {
            var viewModel = new SearchViewModel
            {
                SearchText = model.SearchText
            };

            viewModel.Subtitles = await this.subtitlesService.FindeAsync(model.SearchText, model.Language);

            return View(viewModel);
        }

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
