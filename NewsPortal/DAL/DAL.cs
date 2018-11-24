using NewsPortal.Models;
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
    }
}
