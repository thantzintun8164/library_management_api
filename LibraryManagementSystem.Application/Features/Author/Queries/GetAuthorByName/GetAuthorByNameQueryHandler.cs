using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Contracts.Repositories;
using LibraryManagementSystem.Application.Features.Author.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Application.Features.Author.Queries.GetAuthorByName
{
    internal class GetAuthorByNameQueryHandler : IRequestHandler<GetAuthorByNameQuery, AuthorDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAuthorByNameQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AuthorDto> Handle(GetAuthorByNameQuery request, CancellationToken cancellationToken)
        {
            var authorQuery = _unitOfWork.Authors.GetWithDetails().Where(x => x.Name == request.AuthorName).AsNoTracking();
            var authorDto = await authorQuery.ProjectTo<AuthorDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
            return authorDto ?? throw new NotFoundException($"Author with name:'{request.AuthorName}' not found");

        }
    }
}
