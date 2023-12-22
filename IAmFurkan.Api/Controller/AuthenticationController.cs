using ErrorOr;
using IAmFurkan.Application.Authentication.Commands.Register;
using IAmFurkan.Application.Authentication.Common;
using IAmFurkan.Application.Authentication.Queries.Login;
using IAmFurkan.Contracts.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IAmFurkan.Api.Controller;

[ApiController]
[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediatr;
    private readonly IMapper _mapper;
    public AuthenticationController(ISender mediatr, IMapper mapper)
    {
        _mediatr = mediatr;
        _mapper = mapper;
    }

    [Route("register")]
    public async Task<IActionResult> RegisterAsync(RegisterRequest request)
    {
        RegisterCommand command = _mapper.Map<RegisterCommand>(request);
        ErrorOr<AuthenticationResult> authResult = await _mediatr.Send(command);

        return authResult.Match(
            authResult => Ok(_mapper.Map<AuthenticationResult, AuthenticationResponse>(authResult)),
            errors => Problem(errors));
    }
    [Route("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        LoginQuery query = _mapper.Map<LoginQuery>(request);

        ErrorOr<AuthenticationResult> authResult = await _mediatr.Send(query);

        if (authResult.IsError && authResult.FirstError == Domain.Common.Errors.Errors.Authentication.InvalidCredentials)
        {
            return Problem(statusCode: StatusCodes.Status401Unauthorized, title: authResult.FirstError.Description);
        }

        return authResult.Match(
            authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
            errors => Problem(errors));
    }
}

