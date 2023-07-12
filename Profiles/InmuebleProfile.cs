using AutoMapper;
using NetKubernets.Dtos.ImuebleDtos;
using NetKubernets.Models;

namespace NetKubernets.Profiles;

public class InmuebleProfile : Profile{
    public InmuebleProfile()
    {
        CreateMap<Inmueble, InmuebleResponseDto>();
        CreateMap<InmuebleRequestDto, Inmueble>();
    }
}
