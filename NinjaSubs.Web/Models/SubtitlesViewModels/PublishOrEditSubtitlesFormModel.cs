namespace NinjaSubs.Web.Models.SubtitlesViewModels
{
    using Microsoft.AspNetCore.Http;
    using NinjaSubs.Data.Models.Enums;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class PublishOrEditSubtitlesFormModel
    {
        [Required]
        [StringLength(SubtitlesTitleMaxLength, ErrorMessage = "The {0} must be between {2} and {1} characters.", MinimumLength = SubtitlesTitleMinLength)]
        public string Title { get; set; }

        [Required(ErrorMessage = "The {0} is Required!")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The {0} is Required!")]
        public LanguageType Language { get; set; }

        [Required(ErrorMessage = "The {0} is Required!")]
        public IFormFile File { get; set; }

        [Required(ErrorMessage = "The {0} is Required!")]
        public string Genres { get; set; }
    }
}
