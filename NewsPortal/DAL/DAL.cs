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

        public IEnumerable<News> GetAllNews()
        {
            IEnumerable<News> result = new List<News>();

            using (db = new NewsPortalDBContext())
            {
                result = db.News.ToList();
            }

            return result;
        }

        public async Task<NewsModel> GetNews(int id)
        {
            using (db = new NewsPortalDBContext())
            {
                return this.MapNewsEntityToDto(await db.News.FindAsync(id));
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
                    ValidPeriod = news.ValidPeriod
                };
                await db.News.AddAsync(newsEntity);
                await db.SaveChangesAsync();

                return this.MapNewsEntityToDto(newsEntity);
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
                var news = new News
                {
                    Id = id
                };
                db.News.Attach(news);
                db.News.Remove(news);
                await db.SaveChangesAsync();
            }
        }

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
                Category = new CategoryModel[0], // TODO
                CreateDate = newsEntity.CreateDate,
                Id = newsEntity.Id,
                Lead = newsEntity.Lead,
                Title = newsEntity.Title,
                ValidPeriod = newsEntity.ValidPeriod
            };
        }
    }
}
