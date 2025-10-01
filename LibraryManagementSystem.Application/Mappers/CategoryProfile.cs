using AutoMapper;
using LibraryManagementSystem.Application.Features.Category.Commands.AddCategory;
using LibraryManagementSystem.Application.Features.Category.DTOs;
using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Application.Mappers
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, AddCategoryCommand>().ReverseMap();

            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.NumberOfBooks, opt => opt.MapFrom(src => src.Books.Count()));
        }
    }
}
