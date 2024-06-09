using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbarroLaw.Web.Migrations
{
    /// <inheritdoc />
    public partial class RemovedTagsinEmailTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EmailMessageId",
                table: "Practices",
                type: "uniqueidentifier",
                nullable: true);

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
    }
}
