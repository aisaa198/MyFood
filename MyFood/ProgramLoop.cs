using System;
using System.Collections.Generic;
using System.Linq;
using MyFood.BL.Models;
using MyFood.BL.Services;
using MyFood.Common.Enums;
using MyFood.DAL.Models;
using MyFood.IoHelpers;
using MyFood.MenuOptions;

namespace MyFood
{
    class ProgramLoop
    {
        private readonly Menu _menu;
        private readonly RecipesService _recipesService;
        private readonly GetDataFromUser _getDataFromUser;
        private bool exit = false;

        public ProgramLoop()
        {
            _menu = new Menu();
            _recipesService = new RecipesService();
            _getDataFromUser = new GetDataFromUser();
            _menu.AddOption(new Option("1", "Add receipe", AddReceipe));
            _menu.AddOption(new Option("2", "Show all recipes", ShowAllReceipes));
            _menu.AddOption(new Option("3", "Exit", Exit));
        }

        private void ShowAllReceipes()
        {
            var recipes = _recipesService.ShowAllRecipes();

            foreach(var recipe in recipes)
            {
                Console.Write($"{recipe.Name} ({recipe.Category}) - ");
                foreach(var ingredient in recipe.ListOfIngredients)
                {
                    if (ingredient.Equals(recipe.ListOfIngredients.Last()))
                    {
                        Console.Write($"{ingredient}");
                    }
                    else
                    {
                        Console.Write($"{ingredient},");
                    }
                    
                }
                Console.WriteLine();
            }
        }

        private void AddReceipe()
        {
            Console.WriteLine("Choose the category: ");
            foreach(var option in Enum.GetValues(typeof(Category)))
            {
                Console.WriteLine($"{(int)option} - {option}");
            }
            int.TryParse(Console.ReadLine(), out int numberOfEnumCategory);
            Enum.TryParse<Category>(Enum.GetName(typeof(Category), numberOfEnumCategory), out Category category);

            var nameOFDish = _getDataFromUser.GetData("Give name: ");
            var numberOfIngredients = _getDataFromUser.GetNumber("How many ingredients? ");
            var listOfIngredients = new List<string>();

            for(var i = 1; i <= numberOfIngredients; i++)
            {
                var ingredient = _getDataFromUser.GetData($"{i}: ");
                listOfIngredients.Add(ingredient);
            }

            var directions = _getDataFromUser.GetData("Give directions: ");

            var newRecipe = new RecipeDto
            {
                Id = Guid.NewGuid(),
                Name = nameOFDish,
                Category = category,
                ListOfIngredients = listOfIngredients,
                Directions = directions
            };
            _recipesService.AddRecipe(newRecipe);
        }

        private void Exit()
        {
            exit = true;
        }

        internal void Run()
        {
            while (!exit)
            {
                _menu.PrintOptions();
                Console.Write("Type commad: ");
                var command = Console.ReadLine();
                _menu.InvokeCommand(command);
            }
        }
    }
}
