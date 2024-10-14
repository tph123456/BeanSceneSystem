using System.ComponentModel.DataAnnotations;

namespace BeanSceneSystem.Models
{
    public class Area
    {
        [Key]
        public int AreaID { get; set; }

        [Required]
        [MaxLength(20)]
        public string? AreaName { get; set; }
    }
}
