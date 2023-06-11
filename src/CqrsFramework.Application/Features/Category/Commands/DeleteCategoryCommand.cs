using AutoMapper;
using MediatR;
using CqrsFramework.Application.Features.Category.Models;
using CqrsFramework.Persistance.Context;

namespace CqrsFramework.Application.Features.Category.Commands
{
    public class DeleteCategoryCommand : IRequest<CategoryResponse>
    {
        public DeleteCategoryCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, CategoryResponse>
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public DeleteCategoryCommandHandler(IMapper mapper, DatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<CategoryResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _context.Categories.SingleOrDefault(c => c.Id == request.Id);
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return _mapper.Map<CategoryResponse>(category);
        }
    }
}
