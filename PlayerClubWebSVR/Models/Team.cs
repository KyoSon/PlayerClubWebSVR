using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace PlayerClubWebSVR.Models
{
    public class Team
    {
        public static int Max_TeamId = 0;

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(20)]
        public string Name { get; set; } = string.Empty;

        public int? Ground { get; set; }

        [StringLength(20)]
        public string? Coach { get; set; } = string.Empty;

        public int? FoundedYear { get; set; }

        [StringLength(50)]
        public string? Region { get; set; } = string.Empty;

        [Required(ErrorMessage = "RugbyUnionId is required.")]
        public int RugbyUnionId { get; set; }
    }
}
