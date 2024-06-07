﻿using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication1.DatabaseContext
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Inventory> Inventory { get; set; }
    }
}
