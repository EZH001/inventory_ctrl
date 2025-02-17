using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using server.Db.Tables;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Db
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<User> Users { get; private set; }
        public DbSet<User_name> User_name { get; private set; }
        public DbSet<Category> Category { get; private set; }
        public DbSet<StorageUnit> StorageUnits { get; private set; }
        public DbSet<Product> Products { get; private set; }
        public DbSet<Storage> Storage { get; private set; }
        public DbSet<Supplier> Suppliers { get; private set; }
        public DbSet<Staff> Staff { get; private set; }
        public DbSet<Extradition> Extradition { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Storage>().HasOne(p => p.Category)
                                                    .WithMany()
                                                     .HasForeignKey(p => p.category_id);
            modelBuilder.Entity<User_name>().HasOne(p => p.User)
                                                      .WithMany()
                                                      .HasForeignKey(p => p.User_id);
            modelBuilder.Entity<Product>().HasOne(p => p.StorageUnit)
                                          .WithMany()
                                          .HasForeignKey(p => p.StorageUnitsId);
            modelBuilder.Entity<Product>().HasOne(p => p.Category)
                                                      .WithMany()
                                                      .HasForeignKey(p => p.Category_Id);
            modelBuilder.Entity<Product>().HasOne(p => p.User)
                                                      .WithMany()
                                                      .HasForeignKey(p => p.User_id);
            modelBuilder.Entity<Product>().HasOne(p => p.Storage)
                                                       .WithMany()
                                                       .HasForeignKey(p => p.storage_id);
            modelBuilder.Entity<Product>().HasOne(p => p.Supplier)
                                                                   .WithMany()
                                                                   .HasForeignKey(p => p.supplier_id);
            modelBuilder.Entity<Extradition>().HasOne(p => p.Product)
                                                                   .WithMany()
                                                                   .HasForeignKey(p => p.product_id);
            modelBuilder.Entity<Extradition>().HasOne(p => p.Staff)
                                                                   .WithMany()
                                                                   .HasForeignKey(p => p.staff_id);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
