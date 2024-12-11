using AutoMapper;
using BlogTemplate.Application.Shared.Services.Auth.Dtos;
using BlogTemplate.Domain.Models;


namespace BlogTemplate.Application.Extensions
{
    public static class AutoMapperExtension
    {
        public static IMapper InitMapper(this IMapper mapper)
        {
            if (mapper == null)
            {
                var config = new MapperConfiguration(cfg =>
                  cfg.CreateMap<RegisterDto, ApplicationUser>().ReverseMap()
                );

                mapper = new Mapper(config);
            }

            return mapper;
        }
    }
}
