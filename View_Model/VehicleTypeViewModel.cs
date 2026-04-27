using System.ComponentModel.DataAnnotations;
using Trail_A.Models;

namespace Trail_A.View_Model
{
    public class VehicleTypeViewModel
    {
        [Key]
        public int VehicleTypeId { get; set; }
        [Required]
        public string VehicleName { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int BaseFare { get; set; }
        public int CreatedByUserId { get; set; }
        public List<User> users { get; set; } = new List<User>();
    }
}
