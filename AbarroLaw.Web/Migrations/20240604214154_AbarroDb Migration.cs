using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbarroLaw.Web.Migrations
{
    /// <inheritdoc />
    public partial class AbarroDbMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CasePosts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CaseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Heading = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeaturedImg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeaturedImgURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublishedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Visible = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CasePosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Practices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PracticeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PracticeDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PracticeImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PracticeImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Visible = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Practices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CasePostPractice",
                columns: table => new
                {
                    CasePostsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PracticesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CasePostPractice", x => new { x.CasePostsId, x.PracticesId });
                    table.ForeignKey(
                        name: "FK_CasePostPractice_CasePosts_CasePostsId",
                        column: x => x.CasePostsId,
                        principalTable: "CasePosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CasePostPractice_Practices_PracticesId",
                        column: x => x.PracticesId,
                        principalTable: "Practices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CasePostPractice_PracticesId",
                table: "CasePostPractice",
                column: "PracticesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CasePostPractice");

            migrationBuilder.DropTable(
                name: "CasePosts");

            migrationBuilder.DropTable(
                name: "Practices");
        }
    }
}
