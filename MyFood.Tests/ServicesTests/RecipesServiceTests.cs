using AutoMapper;
using Moq;
using MyFood.BL.Models;
using MyFood.BL.Services;
using MyFood.DAL.Repositories;
using NUnit.Framework;
using System.Collections.Generic;

namespace MyFood.Tests.ServicesTests
{
    [TestFixture]
    class RecipesServiceTests
    {
        private List<RecipeDto> recipesList = new List<RecipeDto>
            {
                new RecipeDto
                {
                    Name = "pomidorowa",
                    ListOfIngredients = new string[]
                    { "pomidory", "woda" }
                },
                new RecipeDto
                {
                    Name = "krupnik",
                    ListOfIngredients = new string[]
                    { "ziemniaki", "woda" }
                },
                new RecipeDto
                {
                    Name = "zapiekanka",
                    ListOfIngredients = new string[]
                    { "bułka", "ser" }
                }
            };

        [Test]
        public void SearchRecipes_null_allRecipes()
        {
            //Arrange
            var mapperMock = new Mock<IMapper>();
            var recipesRepositoryMock = new Mock<IRecipesRepository>();
            var recipesServiceMock = new Mock<RecipesService>(recipesRepositoryMock.Object, mapperMock.Object);
            recipesServiceMock.CallBase = true;
            recipesServiceMock.Setup(x => x.GetRecipes(0)).Returns(recipesList);
            //Act
            var list = recipesServiceMock.Object.SearchRecipes(null);
            //Assert
            recipesServiceMock.Verify(x => x.GetRecipes(0));
            Assert.AreEqual("pomidorowa", list[0].Name);
            Assert.AreEqual("krupnik", list[1].Name);
            Assert.AreEqual(3, list.Count);
        }

        [Test]
        public void SearchRecipes_emptyList_allRecipes()
        {
            //Arrange
            var mapperMock = new Mock<IMapper>();
            var recipesRepositoryMock = new Mock<IRecipesRepository>();
            var recipesServiceMock = new Mock<RecipesService>(recipesRepositoryMock.Object, mapperMock.Object);
            recipesServiceMock.CallBase = true;
            recipesServiceMock.Setup(x => x.GetRecipes(0)).Returns(recipesList);
            var ingredientsList = new string[0];
            //Act
            var list = recipesServiceMock.Object.SearchRecipes(ingredientsList);
            //Assert
            recipesServiceMock.Verify(x => x.GetRecipes(0));
            Assert.AreEqual("pomidorowa", list[0].Name);
            Assert.AreEqual("krupnik", list[1].Name);
            Assert.AreEqual(3, list.Count);
        }

        [Test]
        public void SearchRecipes_listWithTwoIngredients_goodOrder()
        {
            //Arrange
            var mapperMock = new Mock<IMapper>();
            var recipesRepositoryMock = new Mock<IRecipesRepository>();
            var recipesServiceMock = new Mock<RecipesService>(recipesRepositoryMock.Object, mapperMock.Object);
            recipesServiceMock.CallBase = true;
            recipesServiceMock.Setup(x => x.GetRecipes(0)).Returns(recipesList);
            var ingredientsList = new string[] { "ziemniaki", "woda" };
            //Act
            var list = recipesServiceMock.Object.SearchRecipes(ingredientsList);
            //Assert
            recipesServiceMock.Verify(x => x.GetRecipes(0));
            Assert.AreEqual("pomidorowa", list[1].Name);
            Assert.AreEqual("krupnik", list[0].Name);
            Assert.AreEqual(2, list.Count);
        }
    }
}
