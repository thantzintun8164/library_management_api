using AutoMapper;
using LibraryManagementSystem.Application.Features.Author.DTOs;
using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Application.Mappers
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorDto>()
                .ForMember(dest => dest.NumberOfBooks, opt => opt.MapFrom(src => src.Books.Count()));
        }
    }
}
