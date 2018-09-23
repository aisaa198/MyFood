using AutoMapper;
using MyFood.BL.Models;
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

        public List<RecipeDto> ShowAllRecipes()
        {
            return _recipesRepository.ShowAllRecipes().Select(recipe => Mapper.Map<RecipeDto>(recipe)).ToList();
        }
    }
}
