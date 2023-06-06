using Domain.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Empl.Commands.UpdateCommands
{
    public class ConfirmMentorCommand : IRequest<PersonResponse>
    {
        [Required(ErrorMessage = "MentorId is required.")]
        public int MentorId { get; set; }
    }
}
