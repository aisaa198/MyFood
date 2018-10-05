using System;
using MyFood.BL.Models;

namespace MyFood.BL.Services
{
    public interface IRatesService
    {
        Guid AddRate(RateDto rateDto);
        double GetRate(Guid id);
    }
}