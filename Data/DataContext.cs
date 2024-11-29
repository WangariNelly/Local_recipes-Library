using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LocalRecipes.Models;
using Microsoft.EntityFrameworkCore;

namespace LocalRecipes.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Comment> Comments { get; set; } 
    }
}