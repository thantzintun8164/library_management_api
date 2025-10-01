using AutoMapper;
using LibraryManagementSystem.Application.Features.Account.DTOs;
using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Application.Mappers
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<ApplicationUser, UserDto>();
        }
    }
}
