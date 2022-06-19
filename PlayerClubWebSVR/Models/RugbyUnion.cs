using System.ComponentModel.DataAnnotations;

namespace PlayerClubWebSVR.Models
{
    public class RugbyUnion
    {

        public static int Max_RugbyUnionId = 0;

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(20)]
        public string Name { get; set; } = String.Empty;
    }
}
