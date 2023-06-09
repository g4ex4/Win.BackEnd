﻿using Application.Interfaces;
using Application.Services;
using Domain.Common;
using Domain.Entities;
using Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Application.Empl.Commands.UpdateCommands
{
    public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, PersonResponse>
    {
        private readonly IEmployeeDbContext _dbContext;
        private readonly EmailService _emailService;
        private readonly IPasswordHasher<Employee> _passwordHasher;

        public ResetPasswordHandler(IEmployeeDbContext dbContext, EmailService emailService, IPasswordHasher<Employee> passwordHasher)
        {
            _dbContext = dbContext;
            _emailService = emailService;
            _passwordHasher = passwordHasher;
        }

        public async Task<PersonResponse> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user != null)
            {
                const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_-+=<>?";

                StringBuilder sb = new StringBuilder();
                Random rnd = new Random();

                for (int i = 0; i < 8; i++)
                {
                    int index = rnd.Next(chars.Length);
                    sb.Append(chars[index]);
                }
                string hashedPassword = _passwordHasher.HashPassword(user, sb.ToString());
                user.PasswordHash = hashedPassword;

                await _dbContext.SaveChangesAsync(cancellationToken);
                await _emailService.SendEmailResetPasswordAsync(request.Email, sb.ToString());

                var person = new Person
                {
                    Id = user.Id,
                    UserName = user.UserName
                };

                return new PersonResponse(200, "A temporary password has been sent to your email", true, person);
            }

            return new PersonResponse(400, "User not found", false, null);
        }
    }
}
