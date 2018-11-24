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
        [StringLength(256)]
        public string UserName { get; set; }
        [Required]
        [StringLength(256)]
        public string PasswordHash { get; set; }
        [Required]
        [StringLength(256)]
        public string Salt { get; set; }
    }
}
