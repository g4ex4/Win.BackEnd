using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class Person : IdentityUser<int>
    {
        public bool IsDeleted { get; set; }
        public DateTime DateTimeAdded { get; set; }
        public DateTime DateTimeUpdated { get; set; }
    }
}
