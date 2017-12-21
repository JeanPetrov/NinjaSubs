namespace NinjaSubs.Web.Areas.Blog.Models.Articles
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class PublishOrEditArticleFormModel
    {
        [Required]
        [StringLength(ArticleTitleMaxLenght, ErrorMessage = "The {0} must be between {2} and {1} characters." ,MinimumLength = ArticleTitleMinLenght)]
        public string Title { get; set; }

        [Required(ErrorMessage = "The {0} is Required!")]
        public string Content { get; set; }
    }
}
