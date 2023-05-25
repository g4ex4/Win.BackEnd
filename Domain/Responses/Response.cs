using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domain.Responses
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public string JwtToken { get; set; } // Добавленное свойство для хранения токена JWT

        public Response(int statusCode, string message, bool isSuccess)
        {
            StatusCode = statusCode;
            Message = message;
            IsSuccess = isSuccess;
        }

        public Response(int statusCode, string message, bool isSuccess, string jwtToken)
            : this(statusCode, message, isSuccess)
        {
            JwtToken = jwtToken;
        }
    }
}
