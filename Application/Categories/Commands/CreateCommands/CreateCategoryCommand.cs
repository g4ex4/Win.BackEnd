using Domain.Responses;
using MediatR;

namespace Application.Categories.Commands.CreateCommands
{
    public class CreateCategoryCommand : IRequest<Response>
    {
        public string Name { get; set; }
    }
}
