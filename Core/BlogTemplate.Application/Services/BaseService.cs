using AutoMapper;
using BlogTemplate.Application.Extensions;

namespace BlogTemplate.Application.Services
{
    public class BaseService
    {
        public static IMapper Mapper { get; private set; }

        public BaseService()
        {
            Mapper = Mapper.InitMapper();
        }
    }
}
