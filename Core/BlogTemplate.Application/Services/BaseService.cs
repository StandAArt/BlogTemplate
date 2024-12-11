using AutoMapper;
using BlogTemplate.Application.Shared.Services.Auth.Dtos;
using BlogTemplate.Domain.Models;

namespace BlogTemplate.Application.Services
{
    public class BaseService
    {
        public static IMapper Mapper { get; private set; }

        public BaseService()
        {
            InitMapper();
        }

        private void InitMapper()
        {
            if(Mapper == null)
            {
                var config = new MapperConfiguration(cfg =>
             cfg.CreateMap<RegisterDto, ApplicationUser>().ReverseMap()
          );

                Mapper = new Mapper(config);
            }
        }
    }
}
