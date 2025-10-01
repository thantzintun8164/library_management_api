using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Contracts.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Category.Commands.RemoveCategory
{
    internal class RemoveCategoryCommandHandler : IRequestHandler<RemoveCategoryCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(RemoveCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.Categories.FindAsync(request.CategoryId);
            if (category == null)
                throw new NotFoundException($"Category with Id '{request.CategoryId}' not found.");

            _unitOfWork.Categories.Remove(category);
            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
