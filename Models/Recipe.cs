using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LocalRecipes.Models
{
    public class Recipe
    {
         [Key]
        public Guid RecipeId { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }  = string.Empty;
        public string Story { get; set; }  = string.Empty;
        public string Ingredients { get; set; }  = string.Empty;
        public string Steps { get; set; }  = string.Empty;
        public string Region { get; set; } = string.Empty;
        public int CookingTime { get; set; }
        public string? Image { get; set; }
        public int Votes { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}