using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZPI_Database.Models;

namespace Backend.Models
{
    public class TeamTopic
    {
        [Required]
        public Team Team { get; set; }
        public int? TopicId { get; set; }
    }
}
