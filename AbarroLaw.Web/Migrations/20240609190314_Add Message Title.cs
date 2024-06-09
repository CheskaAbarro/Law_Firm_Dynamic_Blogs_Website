using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbarroLaw.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddMessageTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MessageTitle",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MessageTitle",
                table: "Messages");
        }
    }
}
