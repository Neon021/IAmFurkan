using ErrorOr;
using IAmFurkan.Application.Services.Authentication;
using IAmFurkan.Application.Services.Authentication.Commands;
using IAmFurkan.Application.Services.Authentication.Queries;
using IAmFurkan.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace IAmFurkan.Api.Controller;

[ApiController]
[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly IAuthenticationCommandService _authenticationCommandService;
    private readonly IAuthenticationQueryService _authenticationQueryService;
    public AuthenticationController(IAuthenticationCommandService authenticationCommandService, IAuthenticationQueryService authenticationQueryService)
    {
        _authenticationCommandService = authenticationCommandService;
        _authenticationQueryService = authenticationQueryService;
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
    public IActionResult Register(RegisterRequest request)
    {
        ErrorOr<AuthenticationResult> authResult = _authenticationCommandService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        return authResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors));
    }
    [Route("login")]
    public IActionResult Login(LoginRequest request)
    {
        ErrorOr<AuthenticationResult> authResult = _authenticationQueryService.Login(
            request.Email,
            request.Password);

        if (authResult.IsError && authResult.FirstError == IAmFurkan.Domain.Common.Errors.Errors.Authentication.InvalidCredentials)
        {
            return Problem(statusCode: StatusCodes.Status401Unauthorized, title: authResult.FirstError.Description);
        }

        return authResult.Match(authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors));
    }
}

