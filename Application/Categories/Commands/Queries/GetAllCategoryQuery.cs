using Application.Courses.Queries.GetCourseList;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Categories.Commands.Queries
{
    public class GetAllCategoryQuery : IRequest<CategoryListVm>
    {
    }
}
