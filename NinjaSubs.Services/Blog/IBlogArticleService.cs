namespace NinjaSubs.Services.Blog
{
    using NinjaSubs.Services.Blog.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBlogArticleService
    {
        Task<IEnumerable<BlogArticleListingServiceModel>> AllAsync(int page = 1);

        Task<int> TotalAsync();

        Task<BlogArticleDetailsServiceModel> ById(int id);

        Task CreateAsync(string title, string content, string authorId);

        Task UpdateArticleAsync(int id, string title, string content);

        Task DeleteArticleAsync(int id);
    }
}
