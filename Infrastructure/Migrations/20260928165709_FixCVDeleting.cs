using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixCVDeleting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CVRecruter_CV_LikedCVsId",
                table: "CVRecruter");

            migrationBuilder.AddForeignKey(
                name: "FK_CVRecruter_CV_LikedCVsId",
                table: "CVRecruter",
                column: "LikedCVsId",
                principalTable: "CV",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CVRecruter_CV_LikedCVsId",
                table: "CVRecruter");

            migrationBuilder.AddForeignKey(
                name: "FK_CVRecruter_CV_LikedCVsId",
                table: "CVRecruter",
                column: "LikedCVsId",
                principalTable: "CV",
                principalColumn: "Id");
        }
    }
}
