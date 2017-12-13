namespace NinjaSubs.Web.Models.HomeViewModels
{
    using NinjaSubs.Data.Models.Enums;

    public class SearchFormModel
    {
        public string SearchText { get; set; } = "";

        public LanguageType Language { get; set; }
    }
}
