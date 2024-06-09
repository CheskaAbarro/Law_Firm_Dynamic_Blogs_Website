using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbarroLaw.Web.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedEmailTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EmailMessageId",
                table: "Practices",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateSent",
                table: "Messages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Practices_EmailMessageId",
                table: "Practices",
                column: "EmailMessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Practices_Messages_EmailMessageId",
                table: "Practices",
                column: "EmailMessageId",
                principalTable: "Messages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Practices_Messages_EmailMessageId",
                table: "Practices");

            migrationBuilder.DropIndex(
                name: "IX_Practices_EmailMessageId",
                table: "Practices");

            migrationBuilder.DropColumn(
                name: "EmailMessageId",
                table: "Practices");

            migrationBuilder.DropColumn(
                name: "DateSent",
                table: "Messages");
        }
    }
}
