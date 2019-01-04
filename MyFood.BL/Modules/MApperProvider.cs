using System.Linq;
using AutoMapper;
using MyFood.BL.Models;
using MyFood.DAL.Models;
using Ninject.Activation;

namespace MyFood.BL.Modules
{
    internal class MapperProvider : Provider<IMapper>
    {
        protected override IMapper CreateInstance(IContext context)
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RecipeDto, Recipe>().ForMember(x => x.Ingredients, opt => opt.MapFrom(src => string.Join(",", src.ListOfIngredients)));
                cfg.CreateMap<Recipe, RecipeDto>().ForMember(dest => dest.ListOfIngredients, m => m.MapFrom(src => src.Ingredients.Split(',').ToList()));
                cfg.CreateMap<Rate, RateDto>();
                cfg.CreateMap<RateDto, Rate>();
            });

            return new Mapper(mapperConfig);
        }
    }
}
