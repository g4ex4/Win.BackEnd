﻿using Domain.Responses;
using MediatR;

namespace Application.Subs.Commands.CreateCommands
{
    public class SubscribeToCourseCommand : IRequest<Response>
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
    }
}
