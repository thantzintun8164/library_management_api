using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Contracts.Services;
using LibraryManagementSystem.Application.Features.Auth.DTOs;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagementSystem.Application.Features.Auth.Commands.Register
{
    internal class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthService _authService;

        public RegisterUserCommandHandler(UserManager<ApplicationUser> userManager, IAuthService authService)
        {
            _userManager = userManager;
            _authService = authService;
        }

        public async Task<AuthDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (await _userManager.FindByEmailAsync(request.Email) != null)
                throw new BadRequestException("Email is already exist");

            var user = new ApplicationUser() { Email = request.Email, FullName = request.FullName, UserName = Guid.NewGuid().ToString(), Role = Role.User };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                throw new BadRequestException(string.Join('\n', result.Errors.Select(x => x.Description)));

            await _userManager.AddToRoleAsync(user, Role.User.ToString());


            var authDto = await _authService.GetAuthDtoWithNewRefreshTokenAsync(user);
            return authDto;
        }
    }
}
