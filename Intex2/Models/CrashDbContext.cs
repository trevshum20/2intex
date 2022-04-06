using System;
using Microsoft.EntityFrameworkCore;

namespace Intex2.Models
{
    public class CrashDbContext : DbContext
    {
        public CrashDbContext(DbContextOptions<CrashDbContext> options) : base(options)
        {

        }

        public DbSet<utah_crashes_table> utah_crashes_table { get; set; }

    }
}
