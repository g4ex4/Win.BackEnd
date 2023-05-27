using Domain.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Subs.Commands.CreateCommands
{
    public class SubscribeToCourseCommand : IRequest<Response>
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
    }
}
