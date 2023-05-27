using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Responses
{
    public class StudentResponse : Response
    {
        //private const string dateFormat = "dd/MM/yyyy HH:mm:ss zz";
        public StudentResponse(int statusCode, string message, bool isSuccess, Person student)
            : base(statusCode, message, isSuccess)
        {
            UserName = student?.UserName ?? "";
            //UserName = employee.UserName;
        }
        public string UserName { get; set; }
    }
}
