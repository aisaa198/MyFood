using System;
using System.Collections.Generic;
using MyFood.Common.Enums;
using MyFood.DAL.Models;

namespace MyFood.DAL.Repositories
{
    public interface IRecipesRepository
    {
        Recipe AddRecipe(Recipe recipe);
        List<Recipe> GetRecipes(Category category);
        bool AddToFavorites(User user, Recipe recipe);
    }
}