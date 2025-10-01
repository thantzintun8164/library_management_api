using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Contracts.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Author.Commands.RemoveAuthor
{
    internal class RemoveAuthorCommandHandler : IRequestHandler<RemoveAuthorCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveAuthorCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(RemoveAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _unitOfWork.Authors.FindAsync(request.AuthorId);
            if (author == null)
                throw new NotFoundException($"Author with Id '{request.AuthorId}' not found.");

            _unitOfWork.Authors.Remove(author);
            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
