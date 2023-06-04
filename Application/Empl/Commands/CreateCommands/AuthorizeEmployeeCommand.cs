﻿using Domain.Responses;
using MediatR;

namespace Application.Empl.Commands.CreateCommands
{
    public class AuthorizeEmployeeCommand : IRequest<EmployeeResponse>
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
