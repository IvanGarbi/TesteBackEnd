using AutoMapper;
using TesteBackEnd.API.ViewModels;
using TesteBackEnd.Core.Models;

namespace TesteBackEnd.API.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<GetClienteViewModel, Cliente>().ReverseMap();
            CreateMap<PostClienteViewModel, Cliente>().ReverseMap();
        }
    }
}
