using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
namespace HotelAdministration.Areas.Identity.Data;

public class HotelAdministrationUser : IdentityUser
{
    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string Firstname {  get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string LastName { get; set; }
}

