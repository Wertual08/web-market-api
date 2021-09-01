using System.Threading.Tasks;
using Api.Authorization;
using Api.Database.Models;
using Api.Domain.Repositories;
using Api.Domain.Requests;
using Api.Domain.Responses;
using Api.Managers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers {
    [ApiController, Route("auth")]
    public class AuthController : ControllerBase {
        private readonly TokenManager TokenService;
        private readonly HashManager HashService;
        private readonly UsersRepository UsersRepository;
        private readonly RefreshTokensRepository RefreshTokensRepository;

        public AuthController(
            TokenManager tokenService, 
            HashManager hashService, 
            UsersRepository usersRepository,
            RefreshTokensRepository refreshTokensRepository
        ) {
            UsersRepository = usersRepository;
            RefreshTokensRepository = refreshTokensRepository;
            TokenService = tokenService;
            HashService = hashService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthorizationResponse>> Register(RegisterRequest request) {
            var user = await UsersRepository.FindAsync(request.Login, request.Email, request.Phone);
            
            if (user != null) {
                if (request.Login == user.Login) {
                    return Conflict(new ErrorResponse { Error = ErrorType.LoginExists });
                }
                if (request.Email == user.Email) {
                    return Conflict(new ErrorResponse { Error = ErrorType.EmailExists });
                }
                if (request.Phone == user.Phone) {
                    return Conflict(new ErrorResponse { Error = ErrorType.PhoneExists });
                }
            }

            user = new User {
                Role = UserRoleId.Basic,
                Login = request.Login,
                Password = HashService.Make(request.Password),
                Email = request.Email,
                Phone = request.Phone,
                Name = request.Name,
                Surname = request.Surname,
            };

            UsersRepository.Create(user);
            await UsersRepository.SaveAsync();

            var refreshToken = new RefreshToken {
                UserId = user.Id,
                Name = TokenService.Make(),
            };
            RefreshTokensRepository.Create(refreshToken);
            await RefreshTokensRepository.SaveAsync();

            (string accessToken, long expiresAt) = TokenService.Encode(new AccessToken {
                UserId = user.Id,
                Login = user.Login,
                UserRole = user.Role,
            });

            return Ok(new AuthorizationResponse {
                RefreshToken = refreshToken.Name,
                AccessToken = accessToken,
                ExpiresAt = expiresAt,
            });
        } 

        [HttpPost("login")]
        public async Task<ActionResult<AuthorizationResponse>> Login(LoginRequest request) {
            var user = await UsersRepository.FindAsync(request.Login, request.Login, request.Login);

            if (user == null) {
                return NotFound();
            }
            if (!HashManager.Check(user.Password, request.Password)) {
                return Unauthorized();
            }

            user.RefreshToken = await RefreshTokensRepository.FindAsync(user.Id);
            if (user.RefreshToken is null) {
                user.RefreshToken = new RefreshToken {
                    UserId = user.Id,
                    Name = TokenService.Make(),
                };
                RefreshTokensRepository.Create(user.RefreshToken);
                await RefreshTokensRepository.SaveAsync();
            }

            (string accessToken, long expiresAt) = TokenService.Encode(new AccessToken {
                UserId = user.Id,
                Login = user.Login,
                UserRole = user.Role,
            });

            return Ok(new AuthorizationResponse {
                RefreshToken = user.RefreshToken.Name,
                AccessToken = accessToken,
                ExpiresAt = expiresAt,
            });
        } 

        [HttpPost("refresh")]
        public async Task<ActionResult<AuthorizationResponse>> Refresh(RefreshRequest request) {
            var user = await UsersRepository.FindByTokenAsync(request.Token);

            if (user == null) {
                return NotFound();
            }
            
            (string accessToken, long expiresAt) = TokenService.Encode(new AccessToken {
                UserId = user.Id,
                Login = user.Login,
                UserRole = user.Role,
            });

            return Ok(new AuthorizationResponse {
                RefreshToken = request.Token,
                AccessToken = accessToken,
                ExpiresAt = expiresAt,
            });
        } 
    }
}