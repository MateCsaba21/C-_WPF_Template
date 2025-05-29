using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pilots_GUI
{

    // Console: Add-Migration Init, Update-Database

    public class AppContext : DbContext
    {
        public DbSet<Pilot> Pilots { get; set; }

        public AppContext()
        {
            
        }

        public AppContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost;Database=f1pilots;Uid=root;Pwd=;", ServerVersion.AutoDetect("Server=localhost;Database=f1pilots;Uid=root;Pwd=;"));
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<CustomerModel>().HasIndex(c => c.Email).IsUnique();
        //    modelBuilder.Entity<CustomerModel>().HasData(
        //        new CustomerModel() { Id = 1, Name = "Norbert", Email = "horinori@jedlik.eu" },
        //        new CustomerModel() { Id = 2, Name = "László", Email = "laszlo23@jedlik.eu" },
        //        new CustomerModel() { Id = 3, Name = "Gábor", Email = "palcika@gmail.com" }
        //    );

        //    modelBuilder.Entity<OrderModel>().HasData(
        //       new { Id = 1, CustomerId = 2, Description = "Kék halál", OrderTime = DateTime.Today.AddDays(-20) },
        //       new { Id = 2, CustomerId = 2, Description = "SSD hiba", OrderTime = DateTime.Today.AddDays(-10) }
        //    );
        //}

        private static AppContext context = null;

        public static AppContext GetContext()
        {
            if (context == null)
            {
                var constStr = ConfigurationManager.ConnectionStrings["f1pilots"].ConnectionString;
                DbContextOptionsBuilder<AppContext> optionsBuilder = new DbContextOptionsBuilder<AppContext>().UseMySql(constStr, ServerVersion.AutoDetect(constStr));
                context = new AppContext(optionsBuilder.Options);
            }
            return context;
        }
    }
}
