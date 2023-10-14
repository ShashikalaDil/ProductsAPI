using Microsoft.EntityFrameworkCore;
using ProductsAPI.Models;

namespace ProductsAPI.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) 
        { 
        }
        
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Product 1",
                    Price = 100,
                    Quantity = 100,
                    Description = "Product 1 Description",
                    ImageUrl =  "",
                    CreateDate = DateTime.Now,
                    
                },
                new Product
                {
                    Id = 2,
                    Name = "Product 2",
                    Price = 200,
                    Quantity = 20,
                    Description = "Product 2 Description",
                    ImageUrl = "",
                    CreateDate = DateTime.Now,

                },
                new Product
                {
                    Id = 3,
                    Name = "Product 3",
                    Price = 300,
                    Quantity = 10,
                    Description = "Product 3 Description",
                    ImageUrl = "",
                    CreateDate = DateTime.Now,

                });
        }
    }
}
