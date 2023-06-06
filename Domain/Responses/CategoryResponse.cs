using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Responses
{
    public class CategoryResponse : Response
    {
        public CategoryResponse(int statusCode, string message, bool isSuccess, Category category)
        : base(statusCode, message, isSuccess)
        {
            Id = category?.Id ?? 0;
            Name = category?.Name ?? "";
        }
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
