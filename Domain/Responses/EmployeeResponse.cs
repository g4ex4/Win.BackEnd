using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Responses
{
    public class EmployeeResponse: Response
    {
        public EmployeeResponse(int statusCode, string message, bool isSuccess, Person employee)
            : base(statusCode, message, isSuccess)
        {
            UserName = employee?.UserName ?? "";
        }
        public string UserName { get; set; }
    }
}
