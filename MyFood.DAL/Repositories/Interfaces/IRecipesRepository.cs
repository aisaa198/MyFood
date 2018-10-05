using System;
using System.Collections.Generic;
using MyFood.Common.Enums;
using MyFood.DAL.Models;

namespace MyFood.DAL.Repositories
{
    public interface IRecipesRepository
    {
        Guid AddRecipe(Recipe recipe);
        List<Recipe> GetRecipes(Category category);
    }
}