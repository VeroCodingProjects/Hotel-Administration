using HotelAdministration.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace HotelAdministration.Models
{
    public class Bookings
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Name of the client")]
        public string NameClient { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Name of the seller")]
        public string NameSeller {get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate{ get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "The room number must be between 1 and 100")]
        [Display(Name = "Room Number")]
        public int NumberRoom {  get; set; }

    }
}
