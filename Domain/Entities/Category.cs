using Domain.Common;
using Domain.Links;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Category : BaseEntity<int>
    {
        public string Name { get; set; }
        public List<CategoryCourse> CategoryCourse { get; set; }
    }
}
