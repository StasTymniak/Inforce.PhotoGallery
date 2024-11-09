using System.ComponentModel.DataAnnotations;

namespace Inforce.PhotoGallery.Api.Context.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(255)]
        public required string Name { get; set; }
    }
}
