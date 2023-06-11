using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using CqrsFramework.Application.Features.Category.Rules;
using CqrsFramework.Application.Features.Category.Models;
using CqrsFramework.Persistance.Context;

namespace CqrsFramework.Application.Features.Category.Queries
{
    public class GetCategoryQuery : IRequest<IEnumerable<CategoryResponse>>
    {
        public int categoryId { get; set; }
    }

    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, IEnumerable<CategoryResponse>>
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public GetCategoryQueryHandler(IMapper mapper, DatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<CategoryResponse>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            GetCategoryQueryGuard.Against(request).MustBePositive();

            var result = _context.Categories.AsQueryable();
            var response = await result.ToListAsync();
            return _mapper.Map<IEnumerable<CategoryResponse>>(response).ToList();

        }
    }
}
