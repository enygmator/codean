using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodeanBlazorDevEnvShare.Models
{
    public class Team
    {
        [Key]
        public string Name { get; set; }

        [Required]
        public List<Member> Members { get; set; }

        public List<Repo> Repos { get; set; }
    }
}
