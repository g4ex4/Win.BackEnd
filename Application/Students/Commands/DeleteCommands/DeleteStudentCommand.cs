using Domain.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Empl.Commands.DeleteCommands
{
    public class DeleteStudentCommand : IRequest<Response>
    {
        public int StudentId { get; set; }
    }
}
