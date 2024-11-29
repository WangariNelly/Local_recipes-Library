using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LocalRecipes.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        public string Userame { get; set; } = string.Empty;   
        public string Email { get; set; } = string.Empty;
        public  string PasswordHash { get; set; } = string.Empty;
        public string? Bio { get; set; } 
        public string? ProfilePicture { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    } 
}