using System.ComponentModel.DataAnnotations;

namespace Quizify.Common.Models
{
    public record UserLoginModel
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        [RegularExpression("^\\d\\d\\d\\d+$", ErrorMessage ="Game pin mus be four or more digits")]
        public required string GamePin { get; set; }
    }
}
