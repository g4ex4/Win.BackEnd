﻿using Domain.Responses;
using MediatR;

namespace Application.Courses.Commands.CreateCommands
{
    public class CreateCourseCommand : IRequest<Response>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int MentorId { get; set; }
    }
}
