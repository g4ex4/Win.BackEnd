using Domain.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Empl.Commands.UpdateCommands
{
    public class ConfirmMentorCommand : IRequest<Response>
    {
        public int MentorId { get; set; }
    }
}
