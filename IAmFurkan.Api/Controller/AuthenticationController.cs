using IAmFurkan.Application.Services.Authentication;
using IAmFurkan.Application.Services.Authentication.Commands;
using IAmFurkan.Application.Services.Authentication.Queries;
using IAmFurkan.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace IAmFurkan.Api.Controller;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationCommandService _authenticationCommandService;
    private readonly IAuthenticationQueryService _authenticationQueryService;
    public AuthenticationController(IAuthenticationCommandService authenticationCommandService, IAuthenticationQueryService authenticationQueryService)
    {
        _authenticationCommandService = authenticationCommandService;
        _authenticationQueryService = authenticationQueryService;
    }

    [Route("register")]
    public IActionResult Register(RegisterRequest request)
    {
        AuthenticationResult? authResult = _authenticationCommandService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        AuthenticationResponse response = new(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.Token);

        return Ok(response);
    }
    [Route("login")]
    public IActionResult Login(LoginRequest request)
    {
        AuthenticationResult? authResult = _authenticationQueryService.Login(
            request.Email,
            request.Password);

        AuthenticationResponse response = new(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.Token);
        return Ok(response);
    }
}

