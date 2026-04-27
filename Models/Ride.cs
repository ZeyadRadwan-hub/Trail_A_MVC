using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trail_A.Models
{
    public class Ride
    {
        [Key]
        public int RideId { get; set; }
        [Required]
        public string PickUp { get; set; }
        [Required]
        public string PickOff { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Fare { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int DriverId {  get; set; }
        [ForeignKey(nameof(DriverId))]
        public Driver? driver { get; set; }

        public int VehicleId { get; set; }
        [ForeignKey(nameof(VehicleId))]
        public VehicleType? vehicleType { get; set; }

        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User? user { get; set; }
    }
}
