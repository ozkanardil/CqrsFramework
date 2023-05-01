using AutoMapper;
using MediatR;
using CqrsFramework.Application.Features.Category.Models;
using CqrsFramework.Domain.Entities;
using CqrsFramework.Persistance.Context;

namespace CqrsFramework.Application.Features.Category.Commands
{
    public class CreateCategoryCommand:IRequest<CategoryResponse>
    {
        public string Name { get; set; }
    }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryResponse>
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(IMapper mapper, DatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<CategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<CategoryEntity>(request);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<CategoryResponse>(category);

        }
    }
}
