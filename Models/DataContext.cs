using System;
using Microsoft.EntityFrameworkCore;

namespace Miraii.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Content> Contents { get; set; }
    }
}
