using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsPortal.Models.CSharpModels
{
    public class NewsModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Lead { get; set; }

        public string Content { get; set; }
        public DateTime? CreateDate { get; set; }
        public int ValidPeriod { get; set; }
        public int Author { get; set; }

        public IEnumerable<int> CategoryIds { get; set; }

        public IEnumerable<CategoryModel> Categories { get; set; }
    }
}
