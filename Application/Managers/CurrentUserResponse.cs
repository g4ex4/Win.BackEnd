using Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Managers
{
    public class CurrentUserResponse : Response
    {
        public CurrentUserResponse(int statusCode, bool success, string message, UserViewModel user) : base(statusCode, message, success )
        {
            CurrentUser = user;
        }

        public UserViewModel CurrentUser { get; set; }
    }
}
