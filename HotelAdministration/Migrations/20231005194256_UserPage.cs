using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelAdministration.Migrations
{
    public partial class UserPage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_UserProfile_UserProfileId",
                table: "Bookings");

            migrationBuilder.DropTable(
                name: "UserProfile");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_UserProfileId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "Bookings");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Bookings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_AspNetUsers_UserId",
                table: "Bookings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_AspNetUsers_UserId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Bookings");

            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserProfileId",
                table: "Bookings",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_UserProfile_UserProfileId",
                table: "Bookings",
                column: "UserProfileId",
                principalTable: "UserProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
