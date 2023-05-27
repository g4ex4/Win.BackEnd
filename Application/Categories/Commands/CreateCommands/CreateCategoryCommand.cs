using Domain.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Categories.Commands.CreateCommands
{
    public class CreateCategoryCommand : IRequest<Response>
    {
        public string Name { get; set; }
    }
}
