using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Videos.Commands.CreateCommands
{
    public class AddVideoCommandValidator : AbstractValidator<AddVideoCommand>
    {
        public AddVideoCommandValidator()
        {
            RuleFor(command => command.CourseId).GreaterThan(0).WithMessage("Invalid CourseId.");
            RuleFor(command => command.VideoFile)
                .NotNull().WithMessage("VideoFile is required.")
                .Must(BeAValidVideo).WithMessage("Invalid video format.");
        }

        private bool BeAValidVideo(IFormFile file)
        {
            if (file == null)
                return false;

            var allowedExtensions = new[] { ".mp4", ".mov", ".avi", ".mkv" }; // Допустимые расширения файлов

            var fileExtension = Path.GetExtension(file.FileName);

            // Проверка расширения файла
            if (string.IsNullOrEmpty(fileExtension) || !allowedExtensions.Contains(fileExtension.ToLower()))
                return false;

            // Проверка типа файла
            if (!file.ContentType.StartsWith("video/"))
                return false;

            return true;
        }
    }
}
