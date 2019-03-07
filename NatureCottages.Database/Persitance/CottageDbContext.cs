using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using NatureCottages.Database.Domain;

namespace NatureCottages.Database.Persitance
{
    public class CottageDbContext : DbContext
    {
        public CottageDbContext(DbContextOptions<CottageDbContext> options) : base (options)
        {
            
        }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Attraction> Attractions { get; set; }

        public DbSet<Cottage> Cottages { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<ImageGroup> ImageGroups { get; set; }

        public DbSet<FacebookPost> FacebookPosts { get; set; }
        
    }
}
