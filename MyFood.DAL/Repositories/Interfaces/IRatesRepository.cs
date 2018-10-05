using System;
using System.Collections.Generic;
using MyFood.DAL.Models;

namespace MyFood.DAL.Repositories
{
    public interface IRatesRepository
    {
        Guid AddRate(Rate rate);
        List<Rate> GetRate(Guid id);
    }
}