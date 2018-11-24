using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsPortal.Models.DatabaseObjects
{
    public partial class Category
    {
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("title")]
        [StringLength(15)]
        public string Title { get; set; }
    }
}
