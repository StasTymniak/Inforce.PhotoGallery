using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Inforce.PhotoGallery.Api.Context.Models
{
    [PrimaryKey(nameof(UserId), nameof(ImageId))]
    public class UserActivity
    {
        [Key, Column(Order = 0)]
        public int UserId { get; set; }

        [Key, Column(Order = 1)]
        public int ImageId { get; set; }

        public bool IsLiked { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

    }
}
