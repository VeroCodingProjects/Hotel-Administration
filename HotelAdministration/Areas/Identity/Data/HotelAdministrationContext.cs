using HotelAdministration.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HotelAdministration.Models;
using System.Reflection.Emit;

namespace HotelAdministration.Data;

public class HotelAdministrationContext : IdentityDbContext<HotelAdministrationUser>
{
    public HotelAdministrationContext(DbContextOptions<HotelAdministrationContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

    public DbSet<HotelAdministration.Models.Bookings>? Bookings { get; set; }
}
