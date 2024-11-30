using System;
using System.ComponentModel.DataAnnotations;

namespace LocalRecipes.Models
{
    public class User
    {
       [Key]
        public Guid UserId { get; set; }

        [Required, MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string? Bio { get; set; }

        public string? ProfilePicture { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

         public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation
        public ICollection<Recipe>? Recipes { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Vote>? Votes { get; set; }
   
    }
}
