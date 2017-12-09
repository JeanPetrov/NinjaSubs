namespace NinjaSubs.Services.Blog.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using NinjaSubs.Data;
    using NinjaSubs.Data.Models;
    using NinjaSubs.Services.Blog.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using static ServiceConstants;

    public class BlogArticleService : IBlogArticleService
    {
        private readonly NinjaSubsDbContext db;

        public BlogArticleService(NinjaSubsDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<BlogArticleListingServiceModel>> AllAsync(int page = 1)
            => await this.db
                .Articles
                .OrderByDescending(a => a.PublishDate)
                .Skip((page - 1) * BlogArticlesPageSize)
                .Take(BlogArticlesPageSize)
                .ProjectTo<BlogArticleListingServiceModel>()
                .ToListAsync();

        public async Task<int> TotalAsync()
            => await this.db.Articles.CountAsync();

        public async Task<BlogArticleDetailsServiceModel> ById(int id)
            => await this.db
                .Articles
                .Where(a => a.Id == id)
                .ProjectTo<BlogArticleDetailsServiceModel>()
                .FirstOrDefaultAsync();

        public async Task CreateAsync(string title, string content, string authorId)
        {
            var article = new Article
            {
                Title = title,
                Content = content,
                PublishDate = DateTime.UtcNow,
                AuthorId = authorId
            };

            this.db.Add(article);

            await this.db.SaveChangesAsync();
        }

        public async Task UpdateArticleAsync(int id, string title, string content)
        {
            var article = await this.db.Articles.FirstOrDefaultAsync(a => a.Id == id);

            if (article == null)
            {
                return;
            }

            article.Title = title;
            article.Content = content;

            await this.db.SaveChangesAsync();
        }

        public async Task DeleteArticleAsync(int id)
        {
            var article = await this.db.Articles.FirstOrDefaultAsync(a => a.Id == id);

            if (article == null)
            {
                return;
            }

            this.db.Articles.Remove(article);

            await this.db.SaveChangesAsync();
        }
    }
}
