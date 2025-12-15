using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Infrastructure.Repositories;
using TestingPlatform.Respones.Auth;
using TestingPlatform.Respones.Student;
using TestingPlatform.Services;

namespace TestingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Auths(IAuthRepository authRepository, IMapper mapper, ITokenSrevices tokenSrevices, IRefreshTokenRepository refreshTokenRepository, IOptions<JwtSettings> options, IStudentRepository studentRepository) : ControllerBase
    {

        private async Task GenerateAndSetRefreshTokenAsync(int userId)
        {
            var refreshToken = tokenSrevices.CreateRefreshToken();
            var expires = DateTime.UtcNow.AddDays(options.Value.RefreshTokenDays);


            await refreshTokenRepository.SaveRefreshTokenAsync(userId, refreshToken, expires);

            Respones.Cookies.Append("refreshToken", refreshToken, new CookieOptions
            {

                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = expires

            });

        }





        [HttpPost]
        public async Task<IActionResult> Authorize([FromBody] AuthRepository auth)
        {
            var userLoginDTO = mapper.Map<UserLoginDto>(auth);
            var user = await authRepository.AuthorizeUser(userLoginDTO);
            var respones = mapper.Map<AuthResponse>(user);
            var student = await studentRepository.GetByUserIdAsync(user.Id);

            if(student != null)
                respones.Student = mapper.Map<StudentRespones>(student);

            await GenerateAndSetRefreshTokenAsync(user.Id);

            var accessToken = TokenService.CreateAccessToken(respones);

            return Ok();
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
            {
                return Unauthorized();
            }

            var refreshTokenDto = await refreshTokenRepository.RevokeTokenAsync(refreshToken);

            if(refreshTokenDto.expires < DateTime.UtcNow)
                return Unauthorized();


            var authResponse = mapper.Map<AuthResponse>(refreshTokenDto.user);

            await GenerateAndSetRefreshTokenAsync(authRespones.Id);

            var accessToken = tokenSrevices.CreateAccessToken(authResponse);

            return Ok(accessToken);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            if (Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
            {
                return Ok();
            }
            await refreshTokenRepository.RevokeTokenAsync(refreshToken);
            Response.Cookies.Delete("refreshToken");
            return Ok();
        }
    }
}
