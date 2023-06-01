using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Empl.Queries
{
    public class GetAllMentorsQuery : IRequest<List<MentorLookupDto>>
    {
    }
}
