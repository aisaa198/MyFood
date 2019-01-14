using AutoMapper;
using MyFood.BL.Models;
using MyFood.DAL.Models;
using MyFood.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyFood.BL.Services
{
    public class RatesService : IRatesService
    {
        private readonly IRatesRepository _ratesRepository;
        private readonly IMapper _mapper;

        public RatesService(IRatesRepository ratesRepository, IMapper mapper)
        {
            _ratesRepository = ratesRepository;
            _mapper = mapper;
        }

        public RateDto AddRate(RateDto rateDto)
        {
            var rate = _mapper.Map<Rate>(rateDto);
            var addedRate = _ratesRepository.AddRate(rate);
            return _mapper.Map<RateDto>(addedRate);
        }

        public List<RateDto> GetRates(Guid recipeId)
        {
            return _ratesRepository.GetRates(recipeId).Select(x => _mapper.Map<RateDto>(x)).ToList();
        }

        public double CountRate(Guid recipeId)
        {
            var rates = GetRates(recipeId);
            var score = (rates.Count == 0) ? 0 : rates.Average(x => x.Score);
            return Math.Round(score, 1);
        }
    }
}
