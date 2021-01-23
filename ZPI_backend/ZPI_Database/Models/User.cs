using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZPI_Database.Models
{
    public enum AccountType
    {
        stud,
        work
    }
    public class User
    {
        [Key]
        public int Id { get; set; }

        // FK
        public int? TeamId { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        [Required]
        public string Name { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        [Required]
        public string Surname { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        [Required]
        public string Email { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        [Required]
        public string Password { get; set; }

        [Column(TypeName = "VARCHAR(10)")]
        [Required]
        public string AccountType { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string Degree { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string FieldOfStudy { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string Index { get; set; }

        [Column(TypeName = "VARCHAR(10)")]
        public string Faculty { get; set; }

        // Foreign Keys

        // User N : 1 relationship with team(Team)
        [ForeignKey("TeamId")]
        public Team Team { get; set; }

        // Collections

        // User 1 : N relationship with teams(Team) for promotor AccountType
        public ICollection<Team> Teams { get; set; } = new List<Team>();

        // User 1 : N relationship with tasks(Task)
        public ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();

        // User(sender) 1 : N relationship with sent messages(Message)
        public ICollection<Message> MessagesSent { get; set; } = new List<Message>();

        // User(receiver) 1 : N relationship with received messages(Message)
        public ICollection<Message> MessagesReceived { get; set; } = new List<Message>();
    }
}
