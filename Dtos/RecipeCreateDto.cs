using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LocalRecipes.Dtos
{
    public class RecipeCreateDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;
 
        [Required]
        public string Story { get; set; }  = string.Empty;

        [Required]
        public string Ingredients { get; set; }  = string.Empty;

        [Required]
        public string Steps { get; set; }  = string.Empty;

        public string Region { get; set; }  = string.Empty;

        [Required]
        public int CookingTime { get; set; }

        public string? Image { get; set; }

          [Required]
       public UserDto User { get; set; } = new UserDto();
    }
}