using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.JWT
{
    public class FilesUpl
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public byte[] fileUpl { get; set; }
    }
}
