using AutoMapper;
using MediatR;
using CqrsFramework.Application.Features.Category.Models;
using CqrsFramework.Domain.Entities;
using CqrsFramework.Persistance.Context;

namespace CqrsFramework.Application.Features.Category.Commands
{
    public class UpdateCategoryCommand : IRequest<CategoryResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryResponse>
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(IMapper mapper, DatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<CategoryResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<CategoryEntity>(request);
            _context.Categories.Update(category);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<CategoryResponse>(category);
        }
    }
}
