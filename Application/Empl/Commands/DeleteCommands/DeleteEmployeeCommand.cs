using Domain.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Empl.Commands.DeleteCommands
{
    public class DeleteEmplCommand : IRequest<Response>
    {
        public int EmployeeId { get; set; }
    }
}
