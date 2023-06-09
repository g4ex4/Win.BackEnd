﻿using Domain.Common;
using Domain.Entities;
using Domain.Links;
using Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Courses.Commands.UpdateCommands
{
    public class UpdateCourseCommand : IRequest<CourseResponse>
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MentorId { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
