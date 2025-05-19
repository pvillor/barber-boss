using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;

namespace BarberBoss.Application.UseCases.Login.DoLogin;

public interface IDoLoginUseCase
{
    public Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request);
}
