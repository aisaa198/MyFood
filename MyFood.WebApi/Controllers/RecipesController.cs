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
            return _recipesService.AddRecipe(recipe);
        }

        [Route("api/recipes/get")]
        public List<RecipeDto> GetRecipes(int id)
        {
            return _recipesService.GetRecipes((Category)id);
        }

        [HttpPost]
        [Route("api/recipes/search")]
        public List<RecipeDto> SearchRecipes(RecipeDto recipe)
        {
            var ingredients = new string[recipe.ListOfIngredients.Length];
            for (int i = 0; i < recipe.ListOfIngredients.Length; i++){
                ingredients[i] = recipe.ListOfIngredients[i];
            }
            
            return _recipesService.SearchRecipes(ingredients);
        }

        [HttpGet]
        [Route("api/recipes/addex")]
        public List<RecipeDto> AddExemplaryRecipes()
        {
            return _recipesService.AddExemplaryRecipes();
        }

    }
}