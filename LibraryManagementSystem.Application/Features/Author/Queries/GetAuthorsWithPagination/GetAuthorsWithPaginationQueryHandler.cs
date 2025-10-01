using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Common.PaginatedResult;
using LibraryManagementSystem.Application.Contracts.Repositories;
using LibraryManagementSystem.Application.Features.Author.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Application.Features.Author.Queries.GetAuthorsWithPagination
{
    internal class GetAuthorsWithPaginationQueryHandler
        : IRequestHandler<GetAuthorsWithPaginationQuery, PaginatedResult<AuthorDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAuthorsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<AuthorDto>> Handle(GetAuthorsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var authorsQuery = _unitOfWork.Authors.GetWithDetails().AsNoTracking();
            var totalCount = await authorsQuery.CountAsync(cancellationToken);

            if (totalCount == 0)
                throw new NotFoundException("No authors found");

            var authorsDto = await authorsQuery
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<AuthorDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return PaginatedResult<AuthorDto>.Create(authorsDto, totalCount, request.PageNumber, request.PageSize, request.BaseUrl);
        }
    }
}
