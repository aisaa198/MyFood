using System;
using System.Collections.Generic;
using MyFood.DAL.Models;

namespace MyFood.DAL.Repositories
{
    public interface IRatesRepository
    {
        Rate AddRate(Rate rate);
        List<Rate> GetRates(Guid recipeId);
    }
}