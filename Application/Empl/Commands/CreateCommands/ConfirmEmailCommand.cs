using Domain.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Empl.Commands.CreateCommands
{
    public class ConfirmEmailCommand : IRequest<Response>
    {
        public string Email { get; set; }
    }
}
