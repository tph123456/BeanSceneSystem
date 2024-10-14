using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BeanSceneSystem.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationID { get; set; }

        [Required]
        public int SittingID { get; set; }

        [Required]
        [MaxLength(100)]
        public string? GuestName { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; }

        [MaxLength(20)]
        public string? Phone { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        public int NumOfGuests { get; set; }

        [Required]
        [MaxLength(20)]
        public string? ReservationSource { get; set; }

        [MaxLength(100)]
        public string? Notes { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; }

        public int? TableID { get; set; }

        public int? MemberID { get; set; }

        [ForeignKey("SittingID")]
        public Schedule? Schedule { get; set; }

        [ForeignKey("TableID")]
        public Table? Table { get; set; }

        //[ForeignKey("MemberID")]
        //public Member? Member { get; set; }
    }
}
