﻿using KenKata.Shared.Models.Entities;
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

        public virtual DbSet<IdentityUser> Users { get; set; }
        public virtual DbSet<ProductEntity> Products { get; set; }

        public virtual DbSet<CategoryEntity> Categories { get; set; }
        public virtual DbSet<ColorEntity> Colors { get; set; }
        public virtual DbSet<ProductInventoryEntity> ProductsInventory { get; set; }
        public virtual DbSet<TeamMemberProfileEntity> TeamMemberProfiles { get; set; }

    }
}
