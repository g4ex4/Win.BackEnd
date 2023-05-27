﻿using Domain.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Students.Commands.CreateCommands
{
    public class AuthorizeStudentCommand : IRequest<Response>
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
    }
}
