﻿using Microsoft.EntityFrameworkCore;
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
        private NewsPortalDBContext db = new NewsPortalDBContext();

        #region news
        public IEnumerable<NewsModel> GetAllNews()
        {
            IList<NewsModel> x = new List<NewsModel>();

            try
            {




                foreach (var newsEntity in db.News)
                {
                    string name;
                    IQueryable<int> ids;


                    name = db.Writers.Where(_ => _.Id == newsEntity.Author).Single().UserName;
                    ids = db.NewsToCategory.Where(_ => _.NewsId == newsEntity.Id).Select(_ => _.CategoryId);


                    x.Add(new NewsModel()
                    {
                        Author = name,
                        Content = newsEntity.Content,
                        CategoryIds = ids,
                        CreateDate = newsEntity.CreateDate,
                        Id = newsEntity.Id,
                        Lead = newsEntity.Lead,
                        Title = newsEntity.Title,
                        ValidPeriod = newsEntity.ValidPeriod
                    });
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return x;

        }

        internal IEnumerable<NewsModel> GetNewsByCategoryId(int v)
        {
            IList<NewsModel> x = new List<NewsModel>();

            try
            {
                foreach (var newsEntity in db.News)
                {
                    if (db.NewsToCategory.Where(_ => _.NewsId == newsEntity.Id && _.CategoryId == v).Count() == 1)
                    {
                        string name;
                        IQueryable<int> ids;


                        name = db.Writers.Where(_ => _.Id == newsEntity.Author).Single().UserName;
                        ids = db.NewsToCategory.Where(_ => _.NewsId == newsEntity.Id).Select(_ => _.CategoryId);


                        x.Add(new NewsModel()
                        {
                            Author = name,
                            Content = newsEntity.Content,
                            CategoryIds = ids,
                            CreateDate = newsEntity.CreateDate,
                            Id = newsEntity.Id,
                            Lead = newsEntity.Lead,
                            Title = newsEntity.Title,
                            ValidPeriod = newsEntity.ValidPeriod
                        });
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return x;
        }

        public IEnumerable<NewsModel> GetValidNews()
        {
            IList<NewsModel> x = new List<NewsModel>();


            foreach (var newsEntity in db.News.Where(news => news.CreateDate.AddDays(news.ValidPeriod) > DateTime.Now))
            {
                string name;
                IQueryable<int> ids;


                name = db.Writers.Where(_ => _.Id == newsEntity.Author).Single().UserName;
                ids = db.NewsToCategory.Where(_ => _.NewsId == newsEntity.Id).Select(_ => _.CategoryId);


                x.Add(new NewsModel()
                {
                    Author = name,
                    Content = newsEntity.Content,
                    CategoryIds = ids,
                    CreateDate = newsEntity.CreateDate,
                    Id = newsEntity.Id,
                    Lead = newsEntity.Lead,
                    Title = newsEntity.Title,
                    ValidPeriod = newsEntity.ValidPeriod
                });
            }


            return x;


        }

        public NewsModel GetNews(int id)
        {

            NewsModel x = null;
            try
            {


                foreach (var newsEntity in db.News.Where(_ => _.Id == id))
                {
                    string name;
                    IQueryable<int> ids;


                    name = db.Writers.Where(_ => _.Id == newsEntity.Author).Single().UserName;
                    ids = db.NewsToCategory.Where(_ => _.NewsId == newsEntity.Id).Select(_ => _.CategoryId);


                    x = new NewsModel()
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


            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return x;

        }

        public async Task<NewsModel> CreateNews(NewsModel news)
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
                await db.NewsToCategory.AddAsync(new NewsToCategory() { CategoryId = item, NewsId = newsEntity.Id, Id = 0 });
            }

            await db.SaveChangesAsync();

            return news;

        }

        public async Task UpdateNews(int id, NewsModel news)
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

        public IEnumerable<NewsListItemModel> SearchNews(string searchTerm)
        {

            var result = db.News
                .Where(news => news.Title.Contains(searchTerm) || news.Lead.Contains(searchTerm) || news.Content.Contains(searchTerm))
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

        public async Task DeleteNews(int id)
        {

            var connectionsToDelete = db.NewsToCategory.Where(ntc => ntc.NewsId == id);
            db.NewsToCategory.RemoveRange(connectionsToDelete);
            await db.SaveChangesAsync();

            var newsToDelete = new News { Id = id };
            db.News.Attach(newsToDelete);
            db.News.Remove(newsToDelete);

            await db.SaveChangesAsync();

        }
        #endregion

        #region categories

        public IEnumerable<CategoryModel> GetAllCategories()
        {
            IEnumerable<Category> result = new List<Category>();
            try
            {

                result = db.Category.ToList();


            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return result.Select(category => this.MapCategoryEntityToDto(category));
        }

        public CategoryModel GetCategory(int id)
        {

            return this.MapCategoryEntityToDto(db.Category.Find(id));

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

            var categoryToRemove = await db.Category
                .SingleOrDefaultAsync(c => c.Id == id);


            db.NewsToCategory.RemoveRange(db.NewsToCategory.Where(_ => _.CategoryId == id));

            await db.SaveChangesAsync();

            db.Category.Remove(categoryToRemove);


            await db.SaveChangesAsync();

        }

        #endregion

        public Writers GetUser(string userName)
        {

            try
            {
                var user = db.Writers.Where(_ => _.UserName.Equals(userName)).Single();

                return user;
            }
            catch (Exception e)
            {

                throw new NotFoundException();
            }

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
