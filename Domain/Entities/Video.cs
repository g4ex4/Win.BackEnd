using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Video : BaseEntity<int>
    {
        public string VideoName { get; set; }
        public string? Url { get; set; }
        public byte[] Media { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
