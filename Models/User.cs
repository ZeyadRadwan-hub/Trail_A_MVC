using System.ComponentModel.DataAnnotations;

namespace Trail_A.Models
{
    public class User
    {
        [Key]
        public int UserId {  get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        [StringLength(maximumLength:int.MaxValue,MinimumLength =9)]
        public string Password { get; set; }
        [Required]
        [Phone]
        [StringLength(maximumLength:11,MinimumLength =11)]
        public string Phone { get; set; }
        [Required]
        public string Role {  get; set; }
        public List<Driver> drivers { get; set; } = new List<Driver>();
        public List<VehicleType> vehicleTypes { get; set; } = new List<VehicleType>();
        public List<Ride> rides { get; set; } = new List<Ride>();
    }
}
