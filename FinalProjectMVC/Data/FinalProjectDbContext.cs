using FinalProjectMVC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMVC.Data
{
    public class FinalProjectDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserPrefs> Preferences { get; set; }

        public FinalProjectDbContext(DbContextOptions<FinalProjectDbContext> options)
            : base(options)
        { }
    }
}
