using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Contracts.Repositories;
using LibraryManagementSystem.Application.Features.Author.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Application.Features.Author.Queries.GetAuthorsByCategoryName
{
    internal class GetAuthorsByCategoryNameQueryHandler : IRequestHandler<GetAuthorsByCategoryNameQuery, List<AuthorDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAuthorsByCategoryNameQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<AuthorDto>> Handle(GetAuthorsByCategoryNameQuery request, CancellationToken cancellationToken)
        {
            var authorsQuery = _unitOfWork.Books.GetAll(x => x.Category.Name == request.CategoryName).Select(x => x.Author).AsNoTracking();
            var authorsDto = await authorsQuery.ProjectTo<AuthorDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
            return authorsDto.Any() ? authorsDto : throw new NotFoundException($"No authors found in category '{request.CategoryName}'.");

        }
    }
}

