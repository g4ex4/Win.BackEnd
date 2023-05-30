﻿using Application.Interfaces;
using Application.Services;
using Domain.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Application.Students.Commands.UpdateCommands
{
    public class StudentResetPasswordHandler : IRequestHandler<StudentResetPasswordCommand, Response>
    {
        private readonly IStudentDbContext _dbContext;
        private readonly EmailService _emailService;

        public StudentResetPasswordHandler(IStudentDbContext dbContext, EmailService emailService)
        {
            _dbContext = dbContext;
            _emailService = emailService;
        }

        public async Task<Response> Handle(StudentResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Students.FirstOrDefaultAsync(x => x.Email == request.Email);

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
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(sb.ToString());
                user.PasswordHash = hashedPassword;
                await _dbContext.SaveChangesAsync(cancellationToken);
                await _emailService.SendEmailResetPasswordAsync(request.Email, sb.ToString());

                return new Response(200, "A temporary password has been sent to your email", true);
            }
            return new Response(400, "User in not found", true);

        }
    }
}