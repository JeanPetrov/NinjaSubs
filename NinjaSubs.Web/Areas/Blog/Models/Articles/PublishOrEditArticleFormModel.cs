namespace NinjaSubs.Web.Areas.Blog.Models.Articles
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class PublishOrEditArticleFormModel
    {
        [Required]
        [MinLength(ArticleTitleMinLenght)]
        [MaxLength(ArticleTitleMaxLenght)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
