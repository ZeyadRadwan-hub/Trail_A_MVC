using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trail_A.Models;

namespace Trail_A.View_Model
{
    public class RideViewModel
    {
        [Key]
        public int RideId { get; set; }
        [Required]
        public string PickUp { get; set; }
        [Required]
        public string PickOff { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Fare { get; set; }
        [Required]
        public string Status { get; set; }
        public int DriverId { get; set; }

        public int VehicleId { get; set; }

        public int UserId { get; set; }

        public List<Driver> drivers { get; set; } = new List<Driver>();
        public List<VehicleType> vehicleTypes { get; set; } = new List<VehicleType>();
        public List<User> users { get; set; } = new List<User>();
    }
}
