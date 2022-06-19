using System.ComponentModel.DataAnnotations;

namespace PlayerClubWebSVR.Models
{
    public class Player
    {

        public static int Max_PlayerId = 0;

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(20)]
        public string Name { get; set; } = string.Empty;

        [StringLength(10)]
        public string? BirthDate { get; set; } = string.Empty;

        [Range(0, 3, ErrorMessage = "Person's height should be less than 3 meters")]
        public decimal? Height { get; set; }

        public decimal? Weight { get; set; }

        [StringLength(50)]
        public string? PlaceOfBirth { get; set; } = string.Empty ;

        [Required(ErrorMessage = "TeamId is required.")]
        public int TeamId { get; set; }

        [Required(ErrorMessage = "RugbyUnionId is required.")]
        public int RugbyUnionId { get; set; }
    }
}
