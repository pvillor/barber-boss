using BarberBoss.Application.UseCases.Users.GetProfile;
using BarberBoss.Application.UseCases.Users.Register;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberBoss.API.Controllers;

public class UsersController : BarberBossBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterUserUseCase useCase,
        [FromBody] RequestRegisterUserJson request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseUserProfileJson), StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> GetProfile([FromServices] IGetUserProfileUseCase useCase) 
    {
        var response = await useCase.Execute();

        return Ok(response);
    }
}
