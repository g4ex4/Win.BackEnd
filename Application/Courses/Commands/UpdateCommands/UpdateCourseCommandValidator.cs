using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Courses.Commands.UpdateCommands
{
    public class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
    {
        public UpdateCourseCommandValidator()
        {
            RuleFor(command => command.CourseId).GreaterThan(0).WithMessage("Invalid CourseId.");
            RuleFor(command => command.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(command => command.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(command => command.MentorId).GreaterThan(0).WithMessage("Invalid MentorId.");
            RuleFor(command => command.ImageFile)
                .Must(BeAValidImage).WithMessage("Invalid image format.");
        }

        private bool BeAValidImage(IFormFile file)
        {
            if (file == null)
                return true;

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" }; // Допустимые расширения файлов

            var fileExtension = Path.GetExtension(file.FileName);

            // Проверка расширения файла
            if (string.IsNullOrEmpty(fileExtension) || !allowedExtensions.Contains(fileExtension.ToLower()))
                return false;

            // Проверка типа файла
            if (!file.ContentType.StartsWith("image/"))
                return false;

            return true;
        }
    }
}
