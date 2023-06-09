using Domain.Links;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User: IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public string? JobTitle { get; set; }
        public string? Experience { get; set; }
        public string? Education { get; set; }
        public List<UserCourse>? UserCourse { get; set; }
        public Subscription? Subscription { get; set; }
    }
}
