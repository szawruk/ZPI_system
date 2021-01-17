using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZPI_Database.Models
{
    public class Topic
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        [Required]
        public string Name { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        [Required]
        public string Description { get; set; }

        // Collections

        // Topic 1: N relationship with teams(Team) that are doing this topic
        public ICollection<Team> Teams { get; set; }
    }
}
