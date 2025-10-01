using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Contracts.Identity;
using LibraryManagementSystem.Application.Features.Account.DTOs;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Application.Features.Account.Queries.GetUserById
{
    internal class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(ICurrentUserService currentUserService, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _currentUserService = currentUserService;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            if (!_currentUserService.IsInRole(Role.Admin))
                throw new ForbiddenException("Only admins can access users.");

            var userDto = await _userManager.Users.Where(u => u.Id == request.UserId)
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);

            return userDto ?? throw new NotFoundException("User not found");
        }
    }
}