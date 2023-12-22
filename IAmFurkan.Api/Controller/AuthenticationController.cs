using ErrorOr;
using IAmFurkan.Application.Authentication.Commands.Register;
using IAmFurkan.Application.Authentication.Common;
using IAmFurkan.Application.Authentication.Queries.Login;
using IAmFurkan.Contracts.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IAmFurkan.Api.Controller;

[ApiController]
[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediatr;
    public AuthenticationController(ISender mediatr)
    {
        _mediatr = mediatr;
    }

    private static AuthenticationResponse MapAuthResult(AuthenticationResult result)
    {
        return new AuthenticationResponse(result.User.Id,
            result.User.FirstName,
            result.User.LastName,
            result.User.Email,
            result.Token);
    }

    [Route("register")]
    public async Task<IActionResult> RegisterAsync(RegisterRequest request)
    {
        RegisterCommand command = new(request.FirstName, request.LastName, request.Email, request.Password);
        ErrorOr<AuthenticationResult> authResult = await _mediatr.Send(command);

        return authResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors));
    }
    [Route("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        LoginQuery query = new(request.Email, request.Password);

        ErrorOr<AuthenticationResult> authResult = await _mediatr.Send(query);

        if (authResult.IsError && authResult.FirstError == Domain.Common.Errors.Errors.Authentication.InvalidCredentials)
        {
            return Problem(statusCode: StatusCodes.Status401Unauthorized, title: authResult.FirstError.Description);
        }

        return authResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors));
    }
}

