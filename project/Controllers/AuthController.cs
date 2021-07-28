using System.Threading.Tasks;
using Api.Authorization;
using Api.Models;
using Api.Repositories;
using Api.Requests;
using Api.Responses;
using Api.Managers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers {
    [ApiController]
    [Route("auth")]
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
        public async Task<ActionResult<UserResponse>> Register(RegisterRequest request) {
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
                Role = AccessLevel.Basic,
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

            var accessToken = TokenService.Encode(new AccessToken {
                UserId = user.Id,
                Login = user.Login,
                AccessLevel = user.Role,
            });

            return Ok(new AuthorizationResponse {
                RefreshToken = refreshToken.Name,
                AccessToken = accessToken,
            });
        } 

        [HttpPost("login")]
        public async Task<ActionResult<UserResponse>> Login(LoginRequest request) {
            var user = await UsersRepository.FindWithTokenAsync(request.Login, request.Login, request.Login);

            if (user == null) {
                return NotFound();
            }
            if (!HashManager.Check(user.Password, request.Password)) {
                return Unauthorized();
            }

            var accessToken = TokenService.Encode(new AccessToken {
                UserId = user.Id,
                Login = user.Login,
                AccessLevel = user.Role,
            });

            return Ok(new AuthorizationResponse {
                RefreshToken = user.RefreshToken.Name,
                AccessToken = accessToken,
            });
        } 

        [HttpPost("refresh")]
        public async Task<ActionResult<UserResponse>> Refresh(RefreshRequest request) {
            var user = await UsersRepository.FindByTokenAsync(request.Token);

            if (user == null) {
                return NotFound();
            }
            
            var token = TokenService.Encode(new AccessToken {
                UserId = user.Id,
                Login = user.Login,
                AccessLevel = user.Role,
            });

            return Ok(new AuthorizationResponse {
                RefreshToken = request.Token,
                AccessToken = token,
            });
        } 
    }
}