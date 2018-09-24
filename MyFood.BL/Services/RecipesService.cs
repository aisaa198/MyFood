using AutoMapper;
using MyFood.BL.Models;
using MyFood.Common.Enums;
using MyFood.DAL.Models;
using MyFood.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyFood.BL.Services
{
    public class RecipesService
    {
        private readonly RecipesRepository _recipesRepository;

        public RecipesService()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<RecipeDto, Recipe>().ForMember(x => x.Ingredients, opt => opt.MapFrom(src => string.Join(",", src.ListOfIngredients)));
                cfg.CreateMap<Recipe, RecipeDto>().ForMember(dest => dest.ListOfIngredients, m => m.MapFrom(src => src.Ingredients.Split(',').ToList()));
            });
            _recipesRepository = new RecipesRepository();
        }

        public Guid AddRecipe(RecipeDto recipeDto)
        {
            var recipe = Mapper.Map<Recipe>(recipeDto);
            return _recipesRepository.AddRecipe(recipe);
        }

        public List<RecipeDto> GetRecipes(Category category)
        {
            return _recipesRepository.GetRecipes(category).Select(recipe => Mapper.Map<RecipeDto>(recipe)).ToList();
        }

        public List<RecipeDto> SearchRecipes(List<string> listOfIngredients)
        {
            var allRecipes = GetRecipes(0);
            List<RecipeDto> foundRecipes = new List<RecipeDto>();

            foreach (var recipe in allRecipes)
            {
                foreach (var ingredient in recipe.ListOfIngredients)
                {
                    foreach (var element in listOfIngredients)
                    {
                        if (ingredient == element && !foundRecipes.Contains(recipe))
                        {
                            foundRecipes.Add(recipe);
                        }
                    }
                }
            }
            return foundRecipes;
        }
    }
}