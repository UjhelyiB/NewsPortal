using System;

namespace NewsPortal.Models.CSharpModels
{
    public class NewsListItemModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Lead { get; set; }
        public DateTime CreateDate { get; set; }
        public int Author { get; set; }
    }
}
