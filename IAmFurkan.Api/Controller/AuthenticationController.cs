using IAmFurkan.Application.Services.Authentication;
using IAmFurkan.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace IAmFurkan.Api.Controller;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [Route("register")]
    public IActionResult Register(RegisterRequest request)
    {
        AuthenticationResult? authResult = _authenticationService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        AuthenticationResponse response = new(
            authResult.Id,
            authResult.FirstName,
            authResult.LastName,
            authResult.Email,
            authResult.Token);

        return Ok(response);
    }
    [Route("login")]
    public IActionResult Login(LoginRequest request)
    {
        AuthenticationResult? authResult = _authenticationService.Login(
            request.Email,
            request.Password);

        AuthenticationResponse response = new(
            authResult.Id,
            authResult.FirstName,
            authResult.LastName,
            authResult.Email,
            authResult.Token);
        return Ok(response);
    }
}

