using Microsoft.EntityFrameworkCore;
using Generation_Next_IT.Models;

namespace Generation_Next_IT.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<MeetingMinuteMaster>? Meeting_Minutes_Master_Tbl { get; set; }
        public DbSet<CorporateCustomer>? Corporate_Customer_Tbl { get; set; }
        public DbSet<IndividualCustomer>? Individual_Customer_Tbl { get; set; }
        public DbSet<MeetingMinuteDetail>? Meeting_Minutes_Details_Tbl { get; set; }
        public DbSet<ProductService>? Products_Service_Tbl { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MeetingMinuteMaster>()
                .HasOne(m => m.CorporateCustomer)
                .WithMany(cc => cc.MeetingMinuteMasters)
                .HasForeignKey(m => m.CorporateCustomerID);

            modelBuilder.Entity<MeetingMinuteMaster>()
                .HasOne(m => m.IndividualCustomer)
                .WithMany(ic => ic.MeetingMinuteMasters)
                .HasForeignKey(m => m.IndividualCustomerID);

            modelBuilder.Entity<MeetingMinuteDetail>()
                .HasOne(md => md.MeetingMinuteMaster)
                .WithMany(m => m.MeetingMinuteDetails)
                .HasForeignKey(md => md.MeetingMinuteMasterID);

            modelBuilder.Entity<MeetingMinuteDetail>()
                .HasOne(md => md.ProductService)
                .WithMany(ps => ps.MeetingMinuteDetails)
                .HasForeignKey(md => md.ProductServiceID);



            modelBuilder.Entity<ProductService>().HasData(
                new ProductService { ProductServiceID = 1, ProductServiceName = "Laptop", Unit = 5 },
                new ProductService { ProductServiceID = 2, ProductServiceName = "Website making", Unit = 10 },
                new ProductService { ProductServiceID = 3, ProductServiceName = "Mouse", Unit = 8 },
                new ProductService { ProductServiceID = 4, ProductServiceName = "Keyboard", Unit = 15 },
                new ProductService { ProductServiceID = 5, ProductServiceName = "web hosting", Unit = 12 }
            );

            // Seed data for Corporate_Customer_Tbl
            modelBuilder.Entity<CorporateCustomer>().HasData(
                new CorporateCustomer { CorporateCustomerID = 1, CorporateCustomerName = "Ataur" },
                new CorporateCustomer { CorporateCustomerID = 2, CorporateCustomerName = "Shakil" },
                new CorporateCustomer { CorporateCustomerID = 3, CorporateCustomerName = "Rasel" }
            );

            // Seed data for Individual_Customer_Tbl
            modelBuilder.Entity<IndividualCustomer>().HasData(
                new IndividualCustomer { IndividualCustomerID = 1, IndividualCustomerName = "Rakib" },
                new IndividualCustomer { IndividualCustomerID = 2, IndividualCustomerName = "Emran" },
                new IndividualCustomer { IndividualCustomerID = 3, IndividualCustomerName = "Tarek" }
            );

            base.OnModelCreating(modelBuilder);
        }

    }
}
