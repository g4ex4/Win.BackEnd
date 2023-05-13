using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Responses
{
    public class IDentityResponse: Response
    {
        private const string dateFormat = "dd/MM/yyyy HH:mm:ss zz";


        //private Employee employee;

        public IDentityResponse(int statusCode, string message, bool isSuccess, Person person)
            : base(statusCode, message, isSuccess)
        {

            DateTimeAdded = person.DateTimeAdded.ToString(dateFormat);
            DateTimeUpdated = person.DateTimeUpdated.ToString(dateFormat);
        }
        public string DateTimeAdded { get; set; }
        public string DateTimeUpdated { get; set; }
    }
}

