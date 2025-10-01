using AutoMapper;
using LibraryManagementSystem.Application.Contracts.Repositories;
using MediatR;
using CategoryEntity = LibraryManagementSystem.Domain.Entities.Category;
namespace LibraryManagementSystem.Application.Features.Category.Commands.AddCategory
{
    internal class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AddCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<int> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<CategoryEntity>(request);
            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveAsync();
            return category.Id;
        }
    }
}
