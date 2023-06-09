using Domain.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users
{
    public class CheckCodeRequest : IRequest<Response>
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public int Code { get; set; }
    }
}
