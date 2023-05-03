using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Responses
{
    public class EntityResponse<T> : Response
    {
        private const string dateFormat = "dd/MM/yyyy HH:mm:ss zz";
        public EntityResponse(BaseEntity<T> entity, int statusCode, string message, bool isSuccess)
            : base(statusCode, message, isSuccess)
        {
            Id = entity.Id;
            DateTimeAdded = entity.DateTimeAdded.ToString(dateFormat);
            DateTimeUpdated = entity.DateTimeUpdated.ToString(dateFormat);
        }

        public T Id { get; set; }
        public string DateTimeAdded { get; set; }
        public string DateTimeUpdated { get; set; }
    }
}
