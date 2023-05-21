using Domain.Common;
using Domain.Entities;
using Domain.Links;
using Domain.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Empl.Commands.UpdateCommands
{
    public class UpdateEmoloyeeCommand : IRequest<Response>
    {
        [Required]
        public int Id { get; set; }
        //[Required]
        //public string UserName { get; set; }
        //[Required]
        //[EmailAddress(ErrorMessage = "Invalid email format")]
        //public string Email { get; set; }
        //[Required]
        //public string PasswordHash { get; set; }
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public string Experience { get; set; }
        [Required]
        public string Education { get; set; }
        
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
