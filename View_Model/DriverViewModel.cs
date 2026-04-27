using System.ComponentModel.DataAnnotations;
using Trail_A.Models;

namespace Trail_A.View_Model
{
    public class DriverViewModel
    {
        [Key]
        public int DriverId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LicenseNumber { get; set; }
        [Required]
        [Phone]
        [StringLength(maximumLength: 11, MinimumLength = 11)]
        public string Phone { get; set; }
        [Required]
        public string Status { get; set; }
        public int CreatedByUserId { get; set; }
        public List<User> users { get; set; } = new List<User>();
    }
}
