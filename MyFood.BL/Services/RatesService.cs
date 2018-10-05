using AutoMapper;
using MyFood.BL.Models;
using MyFood.DAL.Models;
using MyFood.DAL.Repositories;
using System;
using System.Linq;

namespace MyFood.BL.Services
{
    public class RatesService : IRatesService
    {
        private readonly IRatesRepository _ratesRepository;

        public RatesService(IRatesRepository ratesRepository)
        {
            _ratesRepository = ratesRepository;
        }

        public Guid AddRate(RateDto rateDto)
        {
            var rate = Mapper.Map<Rate>(rateDto);
            return _ratesRepository.AddRate(rate);
        }

        public double GetRate(Guid id)
        {
            var rates = _ratesRepository.GetRate(id);
            var score = (rates == null) ? 0 : rates.Average(x => x.Score);
            return Math.Round(score, 1);
        }
    }
}
