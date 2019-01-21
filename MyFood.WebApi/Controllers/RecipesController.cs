using System;
using System.Collections.Generic;
using System.Web.Http;
using MyFood.BL.Models;
using MyFood.BL.Services.Interfaces;
using MyFood.Common.Enums;

namespace MyFood.WebApi.Controllers
{
    public class RecipesController : ApiController
    {
        private readonly IRecipesService _recipesService;

        public RecipesController(IRecipesService recipesService)
        {
            _recipesService = recipesService;
        }

        [HttpPost]
        [Route("api/recipes/add")]
        public RecipeDto AddRecipe(RecipeDto recipe)
        {
            recipe.Id = Guid.NewGuid();
            return _recipesService.AddRecipe(recipe);
        }

        [Route("api/recipes/get")]
        public List<RecipeDto> GetRecipes(int id)
        {
            return _recipesService.GetRecipes((Category)id);
        }

        [HttpGet]
        [Route("api/recipes/search")]
        public List<RecipeDto> SearchRecipes(string ingredient)
        {
            var ingredients = new string[] { ingredient };
            return _recipesService.SearchRecipes(ingredients);
        }

    }
}