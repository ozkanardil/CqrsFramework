using AutoMapper;
using FluentValidation;
using MediatR;
using CqrsFramework.Application.Features.Category.Models;
using CqrsFramework.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
        }
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
            var category = _context.Categories.SingleOrDefault(c=>c.Id==request.Id);
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return _mapper.Map<CategoryResponse>(category);
        }
    }
}
