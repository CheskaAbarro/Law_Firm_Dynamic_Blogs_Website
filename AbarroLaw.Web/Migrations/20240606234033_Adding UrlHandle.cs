using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbarroLaw.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddingUrlHandle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlHandle",
                table: "CasePosts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlHandle",
                table: "CasePosts");
        }
    }
}
