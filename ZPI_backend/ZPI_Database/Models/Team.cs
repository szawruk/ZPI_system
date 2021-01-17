using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZPI_Database.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }

        // FK
        public int? PromoterId { get; set; }

        // FK
        public int? TopicId { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(255)")]
        public string Name { get; set; }

        [Column(TypeName = "VARCHAR(1024)")]
        public string Description { get; set; }

        [Required]
        public bool Open { get; set; }

        // Foreign Keys

        // Team N : 1 relationship with promoter(User)
        [ForeignKey("PromoterId")]
        public User Promoter { get; set; }
        
        // Team N : 1 relationship with topic(Topic)
        [ForeignKey("TopicId")]
        public Topic Topic { get; set; }

        // Collections

        // Team 1 : N relationship with studens(User)
        public ICollection<User> Students { get; set; }

        // Team 1 : N relationship with tasks(Task)
        public ICollection<ProjectTask> Tasks { get; set; }

        // Team 1 : N relationship with messages(Message)
        public ICollection<Message> Messages { get; set; }
    }
}
