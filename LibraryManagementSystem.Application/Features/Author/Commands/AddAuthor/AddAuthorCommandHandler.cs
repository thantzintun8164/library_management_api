using LibraryManagementSystem.Application.Contracts.Repositories;
using MediatR;
using AuthorEntity = LibraryManagementSystem.Domain.Entities.Author;
namespace LibraryManagementSystem.Application.Features.Author.Commands.AddAuthor
{
    internal class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, int>
    {
        private IUnitOfWork _unitOfWork;

        public AddAuthorCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = new AuthorEntity { Name = request.Name };

            await _unitOfWork.Authors.AddAsync(author);
            await _unitOfWork.SaveAsync();
            return author.Id;
        }
    }
}
