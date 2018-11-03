using NewsPortal.Models;
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
    }
}
