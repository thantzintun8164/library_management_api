using LibraryManagementSystem.API.Common.Responses;
using LibraryManagementSystem.Application.Common.Results;
using LibraryManagementSystem.Application.Features.Auth.Commands.Login;
using LibraryManagementSystem.Application.Features.Auth.Commands.Logout;
using LibraryManagementSystem.Application.Features.Auth.Commands.RefreshToken;
using LibraryManagementSystem.Application.Features.Auth.Commands.Register;
using LibraryManagementSystem.Application.Features.Auth.Commands.RevokeToken;
using LibraryManagementSystem.Application.Features.Author.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LibraryManagementSystem.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [SwaggerTag("Manage user authentication: register, login, logout, refresh token, revoke token, forget and reset password ,and mange your profile")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType<AuthorDto>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Register a new user", Description = "Register a new user and return authentication details with refresh token.")]
        public async Task<IActionResult> Register(RegisterUserCommand command)
        {
            var authDto = await _mediator.Send(command);
            SetRefreshTokenInCookies(authDto.RefreshToken.Token, authDto.RefreshTokenExpiration);
            return Ok(authDto);
        }



        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType<AuthorDto>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Login a user", Description = "Authenticate user credentials and return authentication details with refresh token.")]
        public async Task<IActionResult> Login(LoginUserCommand command)
        {
            var authDto = await _mediator.Send(command);
            SetRefreshTokenInCookies(authDto.RefreshToken.Token, authDto.RefreshTokenExpiration);
            return Ok(authDto);
        }

        [HttpPost("logout")]
        [ProducesResponseType<Result>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Logout user", Description = "Logout the currently authenticated user.")]
        public async Task<IActionResult> Logout()
        {
            await _mediator.Send(new LogoutUserCommand());
            return Ok(Result.Success("Logout Successfully"));
        }


        [HttpPost("refresh-token")]
        [ProducesResponseType<AuthorDto>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Refresh access token", Description = "Refresh the access token using the refresh token stored in cookies.")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["RefreshToken"] ?? "";
            var authDto = await _mediator.Send(new RefreshTokenCommand(refreshToken));
            SetRefreshTokenInCookies(authDto.RefreshToken.Token, authDto.RefreshTokenExpiration);
            return Ok(authDto);
        }



        [HttpPost("revoke-token")]
        [ProducesResponseType<Result>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Revoke refresh token", Description = "Revoke the refresh token for the currently authenticated user.")]
        public async Task<IActionResult> RevokeToken()
        {
            var refreshToken = Request.Cookies["RefreshToken"] ?? "";
            await _mediator.Send(new RevokeTokenCommand(refreshToken));
            return Ok(Result.Success("Token Revoke Successfully"));
        }


        private void SetRefreshTokenInCookies(string refreshToken, DateTime expirationData)
        {
            Response.Cookies.Append("RefreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Expires = expirationData.ToLocalTime(),
                SameSite = SameSiteMode.None,
            });
        }
    }
}
