using AutoMapper;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Entities;

namespace BarberBoss.Application.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestBillingJson, Billing>();
        CreateMap<RequestRegisterUserJson, User>()
            .ForMember(dest => dest.Password, config => config.Ignore());
    }

    private void EntityToResponse()
    {
        CreateMap<Billing, ResponseCreatedBillingJson>()
            .ForMember(dest => dest.BillingId, opt => opt.MapFrom(src => src.Id));
        CreateMap<Billing, ResponseBillingJson>();
        CreateMap<User, ResponseUserProfileJson>();
    }
}
