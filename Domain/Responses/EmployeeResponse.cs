using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Responses
{
    public class EmployeeResponse : Response
    {
        public EmployeeResponse(int statusCode, string message, bool isSuccess, Employee employee)
        : base(statusCode, message, isSuccess)
        {
            Id = employee?.Id ?? 0;
            RoleId = employee?.RoleId ?? 0;
            UserName = employee?.UserName ?? "";
        }
        public string UserName { get; set; }
        public int Id { get; set; }
        public int RoleId { get; set; }


    }
}
