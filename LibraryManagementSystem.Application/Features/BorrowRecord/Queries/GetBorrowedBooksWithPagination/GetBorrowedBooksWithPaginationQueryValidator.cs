using FluentValidation;
using LibraryManagementSystem.Application.Features.BorrowRecord.Queries.GetBorrowedBooksWithPagination;

public class GetBorrowedBooksWithPaginationQueryValidator : AbstractValidator<GetBorrowedBooksWithPaginationQuery>
{
    public GetBorrowedBooksWithPaginationQueryValidator() : base() { }
}
