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
        private readonly IMapper _mapper;

        public RatesService(IRatesRepository ratesRepository, IMapper mapper)
        {
            _ratesRepository = ratesRepository;
            _mapper = mapper;
        }

        public Guid AddRate(RateDto rateDto)
        {
            var rate = _mapper.Map<Rate>(rateDto);
            rate.Id = Guid.NewGuid();
            return _ratesRepository.AddRate(rate);
        }

        public double GetRate(Guid userId)
        {
            var rates = _ratesRepository.GetRate(userId);
            var score = (rates.Count == 0) ? 0 : rates.Average(x => x.Score);
            return Math.Round(score, 1);
        }
    }
}
