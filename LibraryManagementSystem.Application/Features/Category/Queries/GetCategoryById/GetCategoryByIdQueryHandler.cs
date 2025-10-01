using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Contracts.Repositories;
using LibraryManagementSystem.Application.Features.Category.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Application.Features.Category.Queries.GetCategoryById
{
    internal class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var categoryQuery = _unitOfWork.Categories.GetWithDetails().Where(x => x.Id == request.CategoryId).AsNoTracking();
            var categoryDto = await categoryQuery.ProjectTo<CategoryDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
            return categoryDto ?? throw new NotFoundException($"No category found with Id: {request.CategoryId}");
        }
    }
}
