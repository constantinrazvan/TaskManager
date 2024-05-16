﻿using Microsoft.EntityFrameworkCore;
using TaskManager.Server.Models;

namespace TaskManager.Server
{
    public class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Todo> Todos {  get; set; } 
        public DbSet<User> Users { get; set; }
    }
}