using Domain.Common;
using Domain.Links;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
