using Domain.Common;

namespace Domain.Responses
{
    public class StudentResponse : Response
    {
        public StudentResponse(int statusCode, string message, bool isSuccess, Person student)
            : base(statusCode, message, isSuccess)
        {
            UserName = student?.UserName ?? "";
        }
        public string UserName { get; set; }
    }
}
