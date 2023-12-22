using IAmFurkan.Application.Authentication.Commands.Register;
using IAmFurkan.Application.Authentication.Common;
using IAmFurkan.Application.Authentication.Queries.Login;
using IAmFurkan.Contracts.Authentication;
using Mapster;

namespace IAmFurkan.Api.Common.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, RegisterCommand>();
            config.NewConfig<LoginRequest, LoginQuery>();
            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest, src => src.User);
        }
    }
}
