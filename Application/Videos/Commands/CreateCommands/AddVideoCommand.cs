using Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Videos.Commands.CreateCommands
{
    public class AddVideoCommand : IRequest<VideoResponse>
    {
        public int CourseId { get; set; }
        public IFormFile VideoFile { get; set; }
    }
}
