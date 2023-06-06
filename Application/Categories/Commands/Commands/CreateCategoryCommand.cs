using Domain.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Categories.Commands.Commands
{
    public class CreateCategoryCommand : IRequest<CategoryResponse>
    {
        public string Name { get; set; }
    }
}
