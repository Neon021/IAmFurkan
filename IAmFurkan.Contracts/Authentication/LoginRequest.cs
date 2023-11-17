namespace IAmFurkan.Contracts.Authentication;
public record LoginRequest(
    string Email,
    string Password);