using Application.Interfaces;
using Domain.Entities;
using Domain.Responses;
using MediatR;

namespace Application.Categories.Commands.CreateCommands
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Response>
    {
        private readonly ICategoryDbContext _dbContext;

        public CreateCategoryCommandHandler(ICategoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Name = command.Name,
                DateTimeAdded = DateTime.UtcNow,
                DateTimeUpdated = DateTime.UtcNow
            };

            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new Response(200, "Category added successfully", true);
        }
    }
}
