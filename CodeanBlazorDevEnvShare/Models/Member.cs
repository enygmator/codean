using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeanBlazorDevEnvShare.Models
{
    public class Member
    {
        [Key]
        public string Name { get; set; }

        [Required]
        public bool IsAdmin { get; set; }
    }
}
