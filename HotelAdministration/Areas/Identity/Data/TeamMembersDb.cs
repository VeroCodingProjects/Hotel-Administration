using HotelAdministration.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelAdministration.Areas.Identity.Data
{
    public class TeamMembersDb : DbContext
    {
        public TeamMembersDb(DbContextOptions<TeamMembersDb>options) : base(options)
        {
        }
        public DbSet<TeamMembers> Members { get; set; }
    }
}
