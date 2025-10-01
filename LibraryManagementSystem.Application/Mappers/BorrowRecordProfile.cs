using AutoMapper;
using LibraryManagementSystem.Application.Features.BorrowRecord.Commands.BorrowBook;
using LibraryManagementSystem.Application.Features.BorrowRecord.DTOs;
using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Application.Mappers
{
    public class BorrowRecordProfile : Profile
    {
        public BorrowRecordProfile()
        {
            CreateMap<BorrowRecord, BorrowBookCommand>().ReverseMap();

            CreateMap<BorrowRecord, BorrowRecordDto>()
                .ForMember(dest => dest.BookName, opt => opt.MapFrom(src => src.Book.Name))
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User.FullName));
        }
    }
}
