using Microsoft.AspNetCore.Identity;

namespace Domain.Common
{
    public class Person : IdentityUser<int>
    {
        public bool IsDeleted { get; set; }
        public DateTime DateTimeAdded { get; set; }
        public DateTime DateTimeUpdated { get; set; }
    }
}
