using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Responses
{
    public class PersonResponse : Response
    {
        public PersonResponse(int statusCode, string message, bool isSuccess, Person person)
        : base(statusCode, message, isSuccess)
        {
            Id = person?.Id ?? 0;
            UserName = person?.UserName ?? "";
        }
        public string UserName { get; set; }
        public int Id { get; set; }
    }
}
