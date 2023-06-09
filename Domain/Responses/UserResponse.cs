using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Responses
{
    public class UserResponse : JwtResponse
    {
        public UserResponse(int statusCode, string message, bool isSuccess, string jwtToken, User user)
        : base(statusCode, message, isSuccess, jwtToken)
        {
            Id = user.Id;
            UserName = user?.UserName ?? "";
        }
        public string UserName { get; set; }
        public Guid Id { get; set; }
        public int RoleId { get; set; }


    }
}
