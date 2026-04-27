using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trail_A.Models
{
    public class Driver
    {
        [Key]
        public int DriverId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        //Uniqe
        public string LicenseNumber { get; set; }
        [Required]
        [Phone]
        [StringLength(maximumLength: 11, MinimumLength = 11)]
        public string Phone { get; set; }
        [Required]
        public string Status {  get; set; }
        public int CreatedByUserId {  get; set; }
        [ForeignKey(nameof(CreatedByUserId))]
        public User? user { get; set; }
        public List<Ride> rides { get; set; } = new List<Ride>();
    }
}
