using System;
using System.Collections.Generic;
using MyFood.BL.Models;

namespace MyFood.BL.Services
{
    public interface IRatesService
    {
        RateDto AddRate(RateDto rateDto);
        List<RateDto> GetRates(Guid recipeId);
        double CountRate(Guid recipeId);
    }
}