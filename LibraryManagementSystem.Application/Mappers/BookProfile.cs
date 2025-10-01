using AutoMapper;
using LibraryManagementSystem.Application.Features.Book.Commands.AddBook;
using LibraryManagementSystem.Application.Features.Book.DTOs;
using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Application.Mappers
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, AddBookCommand>()
                .ForMember(dest => dest.BookFile, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.NumberOfBorrowRecords, opt => opt.MapFrom(src => src.BorrowRecords.Count()));
        }
    }
}
