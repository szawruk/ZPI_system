using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZPI_Database.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        // FK
        public int SenderId { get; set; }

        // FK
        public int? TeamId { get; set; }

        // FK
        public int? ReceiverId { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(255)")]
        public string Topic { get; set; }

        [Column(TypeName = "VARCHAR(1024)")]
        public string Content { get; set; }

        [Required]
        public DateTime Date { get; set; }

        // Foreign Keys

        // Message N : 1 relationship with sender(User)
        [ForeignKey("SenderId")]
        public User Sender { get; set; }

        // Message N : 1 relationship with team(Team)
        [ForeignKey("TeamId")]
        public Team Team { get; set; }

        // Message N : 1 relationship with receiver(User)
        [ForeignKey("ReceiverId")]
        public User Receiver { get; set; }
    }
}
