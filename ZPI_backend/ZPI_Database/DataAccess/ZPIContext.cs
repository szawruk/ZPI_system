using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZPI_Database.Models;

namespace ZPI_Database.DataAccess
{
    public class ZPIContext: DbContext
    {
        public ZPIContext(DbContextOptions<ZPIContext> options) : base(options)
        {

        }
        public DbSet<Topic> Topics { get; set; }
    }
}
