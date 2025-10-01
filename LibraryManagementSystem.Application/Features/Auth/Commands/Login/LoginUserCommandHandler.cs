using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Contracts.Services;
using LibraryManagementSystem.Application.Features.Auth.DTOs;
using LibraryManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagementSystem.Application.Features.Auth.Commands.Login
{
    internal class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthService _authService;

        public LoginUserCommandHandler(UserManager<ApplicationUser> userManager, IAuthService authService)
        {
            _userManager = userManager;
            _authService = authService;
        }

        public async Task<AuthDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                throw new UnAuthorizedException($"Email or Password incorrect");

            var authDto = await _authService.GetAuthDtoWithExistingRefreshTokenAsync(user);
            return authDto;
        }
    }
}
