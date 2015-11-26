using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace TheWorld.Models
{
    public class WorldConext:DbContext
    {
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Stop> Stops { get; set; }

        public WorldConext()
        {
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
            var cs = Startup.Configuration["Data:TheWorldDB:ConnectionString"];
            option.UseSqlServer(cs);
            base.OnConfiguring(option);
        }     

    }
}
