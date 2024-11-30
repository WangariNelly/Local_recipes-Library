using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LocalRecipes.Models
{
    public class Vote
    {
        [Key]
        public Guid VoteId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Guid RecipeId { get; set; }

        [ForeignKey("RecipeId")]
        public Recipe Recipe { get; set; } = null!;

        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
    }
}