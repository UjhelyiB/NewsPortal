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
                result = db.News.ToList();
            }

            return result.Select(news => this.MapNewsEntityToDto(news));
        }

        public IEnumerable<NewsModel> GetValidNews()
        {
            IEnumerable<News> result = new List<News>();

            using (db = new NewsPortalDBContext())
            {
                result = db.News.Where(news => news.CreateDate.AddDays(news.ValidPeriod) > DateTime.Now)
                    .ToList();
            }

            return result.Select(news => this.MapNewsEntityToDto(news));
        }

        public async Task<NewsModel> GetNews(int id)
        {
            using (db = new NewsPortalDBContext())
            {
                var news = db.News.Where(_ => _.Id == id).SingleOrDefault();
                return this.MapNewsEntityToDto(news);
            }
        }

        public async Task<NewsModel> CreateNews(NewsModel news)
        {
            using (db = new NewsPortalDBContext())
            {
                int userId = db.Writers.Where(_ => _.UserName.Equals(news.Author)).Single().Id;

                // TODO oda vissza mappelést lehetne szebben (pl. nem kézzel :))
                var newsEntity = new News
                {
                    Title = news.Title,
                    Author = userId,
                    Content = news.Content,
                    CreateDate = DateTime.Now,
                    Lead = news.Lead,
                    ValidPeriod = news.ValidPeriod
                };



                await db.News.AddAsync(newsEntity);
                await db.SaveChangesAsync();

                news.Id = newsEntity.Id;

                foreach (var item in news.CategoryIds)
                {
                    await db.NewsToCategory.AddAsync(new NewsToCategory() { CategoryId = item, NewsId = newsEntity.Id });
                }

                await db.SaveChangesAsync();

                return news;
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
                var newsIds = db.NewsToCategory.Where(_ => _.CategoryId == id).Select(_ => _.NewsId);

                var newsToRemove = db.News
                    .Where(news => newsIds.Contains(news.Id))
                    .ToList();



                var categoryToRemove = await db.Category
                    .SingleOrDefaultAsync(c => c.Id == id);

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
            string name;
            IQueryable<int> ids;

            using (db = new NewsPortalDBContext())
            {
                name = db.Writers.Where(_ => _.Id == newsEntity.Author).Single().UserName;
                ids = db.NewsToCategory.Where(_ => _.NewsId == newsEntity.Id).Select(_ => _.CategoryId);

            }
            return new NewsModel
            {
                Author = name,
                Content = newsEntity.Content,
                CategoryIds = ids,
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
