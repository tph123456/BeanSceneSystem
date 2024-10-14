using System.ComponentModel.DataAnnotations;

namespace BeanSceneSystem.Models
{
    public class Table
    {
        [Key]
        public int TableID { get; set; }

        [Required]
        public int AreaID { get; set; }

        [Required]
        [MaxLength(10)]
        public string? TableName { get; set; }

        [Required]
        [MaxLength(6)]
        public string? TableStatus { get; set; }

        public Area? Area { get; set; }
    }
}
