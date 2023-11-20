using IAmFurkan.Domain.Entities;

namespace IAmFurkan.Application.Services.Authentication;

public record AuthenticationResult(
        User User,
        string Token);
