using System.ComponentModel.DataAnnotations;

namespace BeanSceneSystem.Models
{
    public class Schedule
    {

        [Key]
        public int SittingID { get; set; }

        [Required]
        [MaxLength(15)]
        public string? SType { get; set; }

        [Required]
        public DateTime StartDateTime { get; set; }

        [Required]
        public DateTime EndDateTime { get; set; }

        [Required]
        public int SCapacity { get; set; }

        [Required]
        [MaxLength(6)]
        public string? Status { get; set; }
    }
}
