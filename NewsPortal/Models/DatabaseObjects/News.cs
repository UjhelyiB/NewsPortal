using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsPortal.Models.DatabaseObjects
{
    public partial class News
    {
        public News()
        {
            NewsToCategory = new HashSet<NewsToCategory>();
        }

        public int Id { get; set; }
        [Required]
        [Column(TypeName = "ntext")]
        public string Title { get; set; }
        [Required]
        [Column(TypeName = "ntext")]
        public string Lead { get; set; }
        [Required]
        [Column(TypeName = "ntext")]
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public int ValidPeriod { get; set; }
        public int Author { get; set; }

        [InverseProperty("News")]
        public ICollection<NewsToCategory> NewsToCategory { get; set; }
    }
}
