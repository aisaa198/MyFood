using System;
using System.Collections.Generic;
using MyFood.BL.Models;
using MyFood.Common.Enums;

namespace MyFood.BL.Services.Interfaces
{
    public interface IRecipesService
    {
        Guid AddRecipe(RecipeDto recipeDto);
        List<RecipeDto> GetRecipes(Category category);
        List<RecipeDto> SearchRecipes(string[] listOfIngredients);
    }
}