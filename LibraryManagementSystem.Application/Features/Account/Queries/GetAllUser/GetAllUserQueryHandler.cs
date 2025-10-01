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

namespace LibraryManagementSystem.Application.Features.Account.Queries.GetAllUser
{
    internal class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, List<UserDto>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public GetAllUserQueryHandler(ICurrentUserService currentUserService, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _currentUserService = currentUserService;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            if (!_currentUserService.IsInRole(Role.Admin))
                throw new ForbiddenException("Only admins can access all users.");

            var usersQuery = _userManager.Users.AsNoTracking();
            var usersDto = await usersQuery.ProjectTo<UserDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
            return usersDto;
        }
    }
}
