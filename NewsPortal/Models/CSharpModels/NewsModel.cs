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

        public string Context { get; set; }

        public CategoryModel[] Category { get; set; }
    }
}
