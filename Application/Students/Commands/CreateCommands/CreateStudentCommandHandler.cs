﻿using Application.Common.Exceptions;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Responses;
using MediatR;

namespace Application.Empl.Commands.CreateCommands
{
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Response>
    {
        //private readonly UserManager<Employee> _userManager;
        private readonly IStudentDbContext _dbContext;
        private readonly EmailService _emailService;

        public CreateStudentCommandHandler(IStudentDbContext employeeDbContext, EmailService emailService)
        {
            //_userManager = userManager;
            _dbContext = employeeDbContext;
            _emailService = emailService;
        }
        public async Task<Response> Handle(CreateStudentCommand command,
            CancellationToken cancellationToken)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(command.PasswordHash);
            Student student = new Student
            {
                UserName = command.UserName,
                Email = command.Email,
                PasswordHash = hashedPassword,
                DateTimeAdded = DateTime.UtcNow,
                DateTimeUpdated = DateTime.UtcNow,
            };
            await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync(cancellationToken);
            await _emailService.SendStudentEmailAsync(command.Email);
            return new Response(200, "Student added successfully", true);
            
        }

        
    }
}
