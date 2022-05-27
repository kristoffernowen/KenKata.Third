using KenKata.Shared.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KenKata.Shared.Models;

namespace KenKata.WebApp.Data
{
    public class SqlContext : IdentityDbContext
    {
        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {
        }

        protected SqlContext()
        {
        }

        public virtual DbSet<IdentityUser> Users { get; set; }
        public virtual DbSet<ProductEntity> Products { get; set; }

        public virtual DbSet<CategoryEntity> Categories { get; set; }
        public virtual DbSet<ColorEntity> Colors { get; set; }
        public virtual DbSet<ProductInventoryEntity> ProductsInventory { get; set; }
        public virtual DbSet<PostEntity> Posts { get; set; }
        public virtual DbSet<BlogCategoryEntity> BlogCategories { get; set; }
        public virtual DbSet<TagEntity> Tags { get; set; }
        public virtual DbSet<PostTagsEntity> PostTags { get; set; }
    }
}
