using AutoMapper;
using MyFood.BL.Models;
using MyFood.Common.Enums;
using MyFood.DAL.Models;
using MyFood.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using MyFood.BL.Services.Interfaces;

namespace MyFood.BL.Services
{
    public class RecipesService : IRecipesService
    {
        private readonly IRecipesRepository _recipesRepository;

        public RecipesService(IRecipesRepository recipesRepository)
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<RecipeDto, Recipe>().ForMember(x => x.Ingredients, opt => opt.MapFrom(src => string.Join(",", src.ListOfIngredients)));
                cfg.CreateMap<Recipe, RecipeDto>().ForMember(dest => dest.ListOfIngredients, m => m.MapFrom(src => src.Ingredients.Split(',').ToList()));
                cfg.CreateMap<Rate, RateDto>();
                cfg.CreateMap<RateDto, Rate>();
            });
            _recipesRepository = recipesRepository;
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
            Dictionary<RecipeDto, int> foundRecipes = new Dictionary<RecipeDto, int>();
            var counter = 0;

            foreach (var recipe in allRecipes)
            {
                foreach (var ingredient in recipe.ListOfIngredients)
                {
                    foreach (var element in listOfIngredients)
                    {
                        if (ingredient == element)
                        {
                            counter++;
                        }
                    }
                }
                if(counter > 0)
                {
                    foundRecipes.Add(recipe, counter);
                    counter = 0;
                }
            }
            var orderedRecipes = foundRecipes.OrderByDescending(x => x.Value).ToDictionary(pair => pair.Key, pair => pair.Value); ;
            var listOfRecipes = orderedRecipes.Keys.ToList();
            return listOfRecipes;
        }
    }
}