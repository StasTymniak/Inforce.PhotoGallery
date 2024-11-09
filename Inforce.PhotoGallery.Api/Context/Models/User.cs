using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inforce.PhotoGallery.Api.Context.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public required string Username { get; set; }

        [MaxLength(100)]
        public required string Password { get; set; }

        public int? RoleId { get; set; } = null;

        [ForeignKey("RoleId")]
        public Role? Role { get; set; } = null;
    }
}