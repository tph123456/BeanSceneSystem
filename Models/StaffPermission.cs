using System.ComponentModel.DataAnnotations;

namespace BeanSceneSystem.Models
{
    public class StaffPermission
    {
        [Key]
        public int PermissionID { get; set; }

        [Required]
        public int StaffID { get; set; }

        [Required]
        [MaxLength(50)]
        public string? TableName { get; set; }

        [Required]
        [MaxLength(20)]
        public string? PermissionType { get; set; }

        // Navigation property to Staff
        public Staff? Staff { get; set; }
    }
}
