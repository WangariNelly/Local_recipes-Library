using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LocalRecipes.Models
{
    public class Recipe
    {
      [Key]
        public Guid RecipeId { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Story { get; set; } = string.Empty;

        [Required]
        public string Ingredients { get; set; } = string.Empty;

        [Required]
        public string Steps { get; set; } = string.Empty;

        public string Region { get; set; } = string.Empty;

        public int CookingTime { get; set; }

        public string? Image { get; set; }

        public int Votes { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;

        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Vote>? Upvotes { get; set; }
    }
}