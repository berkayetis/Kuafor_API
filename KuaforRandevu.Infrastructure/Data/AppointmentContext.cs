using KuaforRandevu.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class AppointmentContext : DbContext
    {
        public AppointmentContext(DbContextOptions<AppointmentContext> opts) : base(opts) { }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Stylist> Stylists { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Appointment>()
                   .HasIndex(a => new { a.StylistId, a.StartTime })
                   .IsUnique();  // Aynı stylist + aynı başlangıçta çakışmayı engelle
        }
    }
}
