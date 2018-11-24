using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsPortal.Models.DatabaseObjects
{
    public partial class Writers
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "ntext")]
        public string UserName { get; set; }
        [Required]
        [Column(TypeName = "ntext")]
        public string PasswordHash { get; set; }
        [Required]
        [Column(TypeName = "ntext")]
        public string Salt { get; set; }
    }
}
