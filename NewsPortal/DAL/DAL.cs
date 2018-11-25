using Microsoft.EntityFrameworkCore;
using NewsPortal.Models.CSharpModels;
using NewsPortal.Models.DatabaseObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsPortal
{
    public class DAL
    {
        private NewsPortalDBContext db;

        #region news
        public IEnumerable<NewsModel> GetAllNews()
        {
            IEnumerable<News> result = new List<News>();

            using (db = new NewsPortalDBContext())
            {
                result = db.News
                    .Include(news => news.NewsToCategory)
                    .ThenInclude(ntc => ntc.Category)
                    .ToList();
            }

            return result.Select(news => this.MapNewsEntityToDto(news));
        }

        public IEnumerable<NewsModel> GetValidNews()
        {
            IEnumerable<News> result = new List<News>();

            using (db = new NewsPortalDBContext())
            {
                result = db.News
                    .Include(news => news.NewsToCategory)
                    .ThenInclude(ntc => ntc.Category)
                    .Where(news => news.CreateDate.AddDays(news.ValidPeriod) > DateTime.Now)
                    .ToList();
            }

            return result.Select(news => this.MapNewsEntityToDto(news));
        }

        public async Task<NewsModel> GetNews(int id)
        {
            using (db = new NewsPortalDBContext())
            {
                var news = await db.News
                    .Include(n => n.NewsToCategory)
                    .ThenInclude(ntc => ntc.Category)
                    .SingleOrDefaultAsync(n => n.Id == id);
                return this.MapNewsEntityToDto(news);
            }
        }

        public async Task<NewsModel> CreateNews(NewsModel news)
        {
            using (db = new NewsPortalDBContext())
            {
                // TODO oda vissza mappelést lehetne szebben (pl. nem kézzel :))
                var newsEntity = new News
                {
                    Title = news.Title,
                    Author = 1, // TODO
                    Content = news.Content,
                    CreateDate = DateTime.Now,
                    Lead = news.Lead,
                    ValidPeriod = news.ValidPeriod,
                    NewsToCategory = news.CategoryIds.Select(id => new NewsToCategory
                    {
                        CategoryId = id
                    }).ToList()
                };
                await db.News.AddAsync(newsEntity);
                await db.SaveChangesAsync();

                return await this.GetNews(newsEntity.Id);
            }
        }

        public async Task UpdateNews(int id, NewsModel news)
        {
            using (db = new NewsPortalDBContext())
            {
                var dbNews = await db.News.FindAsync(id);

                dbNews.Content = news.Content;
                dbNews.Lead = news.Lead;
                dbNews.Title = news.Title;
                dbNews.ValidPeriod = news.ValidPeriod;
                db.News.Update(dbNews);

                var existingConnections = db.NewsToCategory
                    .Where(ntc => ntc.NewsId == id);

                var connectionsToDelete = existingConnections.Where(ntc => !news.CategoryIds.Contains(ntc.CategoryId));
                db.NewsToCategory.RemoveRange(connectionsToDelete);

                var categoryIdsToConnect = news.CategoryIds.Where(cid => !existingConnections.Any(ntc => ntc.CategoryId == cid));
                foreach (var categoryId in categoryIdsToConnect)
                {
                    await db.NewsToCategory.AddAsync(new NewsToCategory
                    {
                        CategoryId = categoryId,
                        NewsId = id
                    });
                }

                await db.SaveChangesAsync();
            }
        }

        public IEnumerable<NewsListItemModel> SearchNews(string searchTerm)
        {
            using (db = new NewsPortalDBContext())
            {
                var result = db.News
                    .Where(news => news.Title.Contains(searchTerm))
                    .ToList()
                    .Select(news => new NewsListItemModel
                    {
                        Id = news.Id,
                        Title = news.Title,
                        Lead = news.Lead,
                        Author = news.Author,
                        CreateDate = news.CreateDate
                    });

                return result;
            }
        }

        public async Task DeleteNews(int id)
        {
            using (db = new NewsPortalDBContext())
            {
                var connectionsToDelete = db.NewsToCategory.Where(ntc => ntc.NewsId == id);
                db.NewsToCategory.RemoveRange(connectionsToDelete);

                var newsToDelete = new News { Id = id };
                db.News.Attach(newsToDelete);
                db.News.Remove(newsToDelete);

                await db.SaveChangesAsync();
            }
        }
        #endregion

        #region categories

        public IEnumerable<CategoryModel> GetAllCategories()
        {
            IEnumerable<Category> result = new List<Category>();

            using (db = new NewsPortalDBContext())
            {
                result = db.Category.ToList();
            }

            return result.Select(category => this.MapCategoryEntityToDto(category));
        }

        public async Task<CategoryModel> GetCategory(int id)
        {
            using (db = new NewsPortalDBContext())
            {
                return this.MapCategoryEntityToDto(await db.Category.FindAsync(id));
            }
        }

        public async Task<CategoryModel> CreateCategory(CategoryModel category)
        {
            using (db = new NewsPortalDBContext())
            {
                var categoryEntity = new Category
                {
                    Title = category.Title
                };
                await db.Category.AddAsync(categoryEntity);
                await db.SaveChangesAsync();

                return this.MapCategoryEntityToDto(categoryEntity);
            }
        }

        public async Task UpdateCategory(int id, CategoryModel category)
        {
            using (db = new NewsPortalDBContext())
            {
                var dbCategory = await db.Category.FindAsync(id);
                
                dbCategory.Title = category.Title;

                db.Category.Update(dbCategory);
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteCategory(int id)
        {
            using (db = new NewsPortalDBContext())
            {
                var newsToRemove = db.News
                    .Where(news => news.NewsToCategory.All(connection => connection.CategoryId == id))
                    .ToList();
                var categoryToRemove = await db.Category
                    .Include(c => c.NewsToCategory)
                    .SingleOrDefaultAsync(c => c.Id == id);
                var connectionsToRemove = categoryToRemove.NewsToCategory;

                db.NewsToCategory.RemoveRange(connectionsToRemove);
                db.Category.Remove(categoryToRemove);
                db.News.RemoveRange(newsToRemove);

                await db.SaveChangesAsync();
            }
        }

        #endregion

        public bool LoginCorrect(string userName, string passwordHash)
        {
            using (db = new NewsPortalDBContext())
            {
                try
                {
                    db.Writers.Where(_ => _.UserName.Equals(userName) && _.PasswordHash.Equals(passwordHash)).Single();

                    return true;
                }
                catch (Exception)
                {

                    return false;
                }
            }
        }

        public string GetSaltForUser(string userName)
        {
            using (db = new NewsPortalDBContext())
            {
                try
                {
                    var user = db.Writers.Where(_ => _.UserName.Equals(userName)).Single();

                    return user.Salt;
                }
                catch (Exception e)
                {

                    throw new NotFoundException();
                }
            }
        }

        private NewsModel MapNewsEntityToDto(News newsEntity)
        {
            return new NewsModel
            {
                Author = newsEntity.Author,
                Content = newsEntity.Content,
                CategoryIds = newsEntity.NewsToCategory.Select(ntc => ntc.CategoryId),
                Categories = newsEntity.NewsToCategory.Select(ntc => new CategoryModel {
                    Id = ntc.CategoryId,
                    Title = ntc.Category.Title
                }),
                CreateDate = newsEntity.CreateDate,
                Id = newsEntity.Id,
                Lead = newsEntity.Lead,
                Title = newsEntity.Title,
                ValidPeriod = newsEntity.ValidPeriod
            };
        }

        private CategoryModel MapCategoryEntityToDto(Category categoryEntity)
        {
            return new CategoryModel
            {
                Id = categoryEntity.Id,
                Title = categoryEntity.Title
            };
        }
    }
}
