using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LocalRecipes.Models
{
    public class Comment
    {
        [Key]
        public Guid CommentId { get; set; }
         public Guid RecipeId { get; set; }
          public Guid UserId { get; set; }
          public string Text { get; set; } = string.Empty;
          public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}