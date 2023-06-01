using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Empl.Queries
{
    public class GetMentorByIdQuery : IRequest<MentorLookupDto>
    {
        public int MentorId { get; set; }
    }
}
