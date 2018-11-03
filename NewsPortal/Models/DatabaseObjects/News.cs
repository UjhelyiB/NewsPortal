using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsPortal.Models
{
    public partial class News
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "ntext")]
        public string Title { get; set; }
        [Required]
        [Column(TypeName = "ntext")]
        public string Context { get; set; }
    }
}
