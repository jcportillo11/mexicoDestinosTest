using mexicoDestinos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mexicoDestinos.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Shuttle> Shuttles { get; set; }
        public DbSet<ShuttleText> ShuttleTexts { get; set; }
    }
}
