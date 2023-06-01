using Application.Empl.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Students.Queries
{
    
    public class GetAllStudentsQuery : IRequest<List<StudentLookupDto>>
    {
    }
    
}
