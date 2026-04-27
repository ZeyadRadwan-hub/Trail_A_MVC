using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trail_A.Models
{
    public class VehicleType
    {
        [Key]
        public int VehicleTypeId { get; set; }
        [Required]
        public string VehicleName { get; set; }
        [Required]
        [Range(1,int.MaxValue)]
        public int BaseFare { get; set; }
        public int CreatedByUserId { get; set; }
        [ForeignKey(nameof(CreatedByUserId))]
        public User? user { get; set; }
        public List<Ride> Rides { get; set; } = new List<Ride>();

    }
}
