using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LocalRecipes.Data;
using LocalRecipes.Dtos;
using LocalRecipes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocalRecipes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public RecipeController(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpPost("newRecipe")]
        [Authorize]
        public async Task<ActionResult<Recipe>> CreateRecipe([FromBody] RecipeCreateDto recipeDto)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "sub");
    if (userIdClaim == null)
    {
        return Unauthorized("User ID not found in token.");
    }

    var userId = Guid.Parse(userIdClaim.Value);
    var user = await context.Users.FindAsync(userId);

    if (user == null)
    {
        return Unauthorized("User not found.");
    }

    var recipe = mapper.Map<Recipe>(recipeDto);
    recipe.UserId = user.UserId;

    context.Recipes.Add(recipe);
    await context.SaveChangesAsync();
    Console.WriteLine("Saved to Database");

    return CreatedAtAction("GetRecipe", new { id = recipe.RecipeId }, recipe);
        }
    }
}