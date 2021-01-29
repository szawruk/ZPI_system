using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZPI_Database.Models;

namespace Backend.Models
{
    public class TaskUser
    {
        public ProjectTask ProjectTask { get; set; }
        public int StudentId { get; set; }
    }
}
