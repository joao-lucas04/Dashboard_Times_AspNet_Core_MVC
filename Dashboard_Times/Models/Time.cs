using System.ComponentModel.DataAnnotations;

namespace Dashboard_Times.Models
{
    public class Time
    {
        [Key]
        public int IdTime { get; set; }

        [Required(ErrorMessage = "The name is mandatory.")]
        [StringLength(250, ErrorMessage = "The name must have a maximum of 250 characters.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "The abbreviation is mandatory.")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "The abbreviation must be exactly 3 letters long.")]
        [RegularExpression("^[A-Za-z]{3}$", ErrorMessage = "The abbreviation must contain only letters.")]
        public string Abreviacao { get; set;}

        [Required(ErrorMessage = "The image is mandatory.")]
        public string Img { get; set;}
    }
}
