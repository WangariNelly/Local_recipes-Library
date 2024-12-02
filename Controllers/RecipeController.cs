using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using LocalRecipes.Data;
using LocalRecipes.Dtos;
using LocalRecipes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        //  [Authorize]
        public async Task<ActionResult<Recipe>> CreateRecipe([FromBody] RecipeCreateDto recipeDto)
        {
   Console.WriteLine("We made it!");
            foreach (var header in Request.Headers)
    {
        Console.WriteLine($"{header.Key}: {header.Value}");
    }
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "sub");
         if (userIdClaim == null)
    {
        return Unauthorized("User ID not found in token.");
    }

    var userId = Guid.Parse(userIdClaim.Value);
    Console.WriteLine($"UserId from Claims: {userId}");
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

           [HttpGet("get/{id}")]
        public async Task<ActionResult> GetRecipe(Guid id)
        {
            var recipe = await context.Recipes.Include(r => r.Comments).FirstOrDefaultAsync(r => r.RecipeId == id);
            if (recipe == null)
                return NotFound(new { Message = "Recipe not found!" });

            return Ok(recipe);
        }


        [HttpGet("all")]
        [Authorize]
        public async Task<ActionResult> GetAllRecipes()
        {
             Console.WriteLine("Saved to Database");
            var recipes = await context.Recipes.Include(r => r.User).ToListAsync();
            return Ok(recipes);
        }

        [HttpPost("add/{recipeId}")]
[Authorize]
public async Task<ActionResult<Comment>> AddComment(Guid recipeId, [FromBody] CommentDto commentDto)
{
    var recipe = await context.Recipes.FindAsync(recipeId);
    if (recipe == null)
        return NotFound("Recipe not found.");

    var comment = new Comment
    {
        CommentId = Guid.NewGuid(),
        RecipeId = recipeId,
        UserId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value), // Get UserId from the token
        Content = commentDto.Content,
        CreatedAt = DateTime.UtcNow
    };

    context.Comments.Add(comment);
    await context.SaveChangesAsync();

    return CreatedAtAction("GetComments", new { recipeId = recipeId }, comment);
}

[HttpPost("vote/{recipeId}")]
[Authorize]
public async Task<ActionResult<Vote>> AddVote(Guid recipeId, [FromBody] VoteDto voteDto)
{
    var recipe = await context.Recipes.FindAsync(recipeId);
    if (recipe == null)
        return NotFound("Recipe not found.");

    var vote = new Vote
    {
        VoteId = Guid.NewGuid(),
        RecipeId = recipeId,
        UserId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value),
        IsUpvote = voteDto.IsUpvote,
        CreatedAt = DateTime.UtcNow
    };

    context.Votes.Add(vote);
    await context.SaveChangesAsync();

    return CreatedAtAction("GetVotes", new { recipeId = recipeId }, vote);
}


    }
}