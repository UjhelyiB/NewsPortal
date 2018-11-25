using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsPortal.Models.DatabaseObjects
{
    public partial class NewsToCategory
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("newsId")]
        public int NewsId { get; set; }
        [Column("categoryId")]
        public int CategoryId { get; set; }
    }
}
