using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CqrsFramework.Application.Features.Category.Models;
using CqrsFramework.Persistance.Context;
using System.Collections.Generic;

namespace CqrsFramework.Application.Features.Category.Queries
{
    public class GetCategoryQuery:IRequest<IEnumerable<CategoryResponse>>
    {

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
            var result = _context.Categories.AsQueryable();
            var response = await result.ToListAsync();
            return _mapper.Map<IEnumerable<CategoryResponse>>(response).ToList();

        }
    }
}
