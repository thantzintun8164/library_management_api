using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Common.PaginatedResult;
using LibraryManagementSystem.Application.Contracts.Repositories;
using LibraryManagementSystem.Application.Features.Category.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Application.Features.Category.Queries.GetCategoriesWithPagination
{
    public class GetCategoriesWithPaginationQueryHandler
        : IRequestHandler<GetCategoriesWithPaginationQuery, PaginatedResult<CategoryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetCategoriesWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<CategoryDto>> Handle(GetCategoriesWithPaginationQuery request, CancellationToken cancellationToken)
        {
            int pageNumber = request.PageNumber > 0 ? request.PageNumber : 1;
            int pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

            var categoriesQuery = _unitOfWork.Categories.GetWithDetails().AsNoTracking();
            var totalCount = await categoriesQuery.CountAsync();
            if (totalCount == 0)
                throw new NotFoundException("No Categories found");

            var categoriesDto = await categoriesQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize)
                    .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

            return PaginatedResult<CategoryDto>.Create(categoriesDto, totalCount, pageNumber, pageSize, request.BaseUrl);
        }
    }
}

