using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using api_task.Models;
using System;

namespace api_task
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration)
        : base(options) { _configuration = configuration;  }

        public IConfiguration _configuration { get; }
        public DbSet<User> user { get; set; }
        public DbSet<Quest> task { get; set; }
        public DbSet<Category> category { get; set; }
        public DbSet<State> state { get; set; }
        public DbSet<Comment> comment { get; set; }
        public DbSet<Tag> tag { get; set; }
        public DbSet<Priority> priority { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(_configuration.GetConnectionString("MySqlConnection"), ServerVersion.AutoDetect(_configuration.GetConnectionString("MySqlConnection"))));
            services.AddDbContext<AppDbContext>();

        }


    }
}
