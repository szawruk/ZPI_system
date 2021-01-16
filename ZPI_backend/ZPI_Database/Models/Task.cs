using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZPI_Database.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }

        // FK
        public int? StudentId { get; set; }

        // FK
        public int? TeamId { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        [Required]
        public string Name { get; set; }

        [Column(TypeName = "VARCHAR(1024)")]
        public string Description { get; set; }

        [Required]
        public DateTime Deadline { get; set; }

        [Required]
        public bool Finished { get; set; }

        // Foreign Keys

        // 1 : N relationship with student(User)
        [ForeignKey("StudentId")]
        public User Student { get; set; }

        // 1 : N relationship with team(Team)
        [ForeignKey("TeamId")]
        public Team Team { get; set; }
    }
}
