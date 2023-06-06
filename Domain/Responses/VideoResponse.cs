using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Responses
{
    public class VideoResponse : Response
    {
        public VideoResponse(int statusCode, string message, bool isSuccess, Video video)
        : base(statusCode, message, isSuccess)
        {
            VideoName = video?.VideoName ?? "";
            Id = video?.Id ?? 0;
            Url = video?.Url ?? "";
        }
        public string VideoName { get; set; }
        public string Url { get; set; }
        public int Id { get; set; }

    }
}
