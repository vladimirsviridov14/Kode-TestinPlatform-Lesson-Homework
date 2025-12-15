using TestingPlatform.Respones.Auth;

namespace TestingPlatform.Services
{
    public interface ITokenSrevices
    {
        string CreateAccessToken(AuthResponse authResponse);
        string CreateRefreshToken();

    }
}
