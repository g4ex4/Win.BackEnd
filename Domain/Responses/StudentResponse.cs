using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Responses
{
    public class StudentResponse : JwtResponse
    {
        public StudentResponse(int statusCode, string message, bool isSuccess, string jwtToken, Student student)
        : base(statusCode, message, isSuccess, jwtToken)
        {
            Id = student?.Id ?? 0;
            UserName = student?.UserName ?? "";
        }
        public string UserName { get; set; }
        public int Id { get; set; }
    }
}
