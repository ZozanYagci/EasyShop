﻿using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class Context : DbContext
    {
        public const string DEFAULT_SCHEMA = "dbo";

        public Context()
        {

        }

        public Context(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }
        public DbSet<Entities.Concrete.Attribute> Attributes { get; set; }
        public DbSet<AttributeValue> AttributeValues { get; set; }
        public DbSet<ProductAttribute> AttributeAttributes { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<ProductDetailType> ProductDetailTypes { get; set; }
        public DbSet<ProductStock> ProductStocks { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<AuthUser> AuthUsers { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<AuthUserOperationClaim> AuthUserOperationClaims { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connStr = "Server=.\\MSSQLSERVER1;Initial Catalog=EasyShop;Integrated Security=true;TrustServerCertificate=True";

                optionsBuilder.UseSqlServer(connStr, opt =>
                {
                    opt.EnableRetryOnFailure(); //veritabanı bağlantısında geçici bir hata oluşursa Entity Framework Core bu hatayı yönetir ve yeniden deneme (retry) yapar. Bu, veritabanı bağlantısının daha dayanıklı ve güvenilir olmasını sağlar.
                });
            }
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
