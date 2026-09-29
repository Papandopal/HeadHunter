using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixCandidateDeleting1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateSkill_Candidate_CandidateId",
                table: "CandidateSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Candidate_CandidateId",
                table: "Project");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateSkill_Candidate_CandidateId",
                table: "CandidateSkill",
                column: "CandidateId",
                principalTable: "Candidate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Candidate_CandidateId",
                table: "Project",
                column: "CandidateId",
                principalTable: "Candidate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateSkill_Candidate_CandidateId",
                table: "CandidateSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Candidate_CandidateId",
                table: "Project");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateSkill_Candidate_CandidateId",
                table: "CandidateSkill",
                column: "CandidateId",
                principalTable: "Candidate",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Candidate_CandidateId",
                table: "Project",
                column: "CandidateId",
                principalTable: "Candidate",
                principalColumn: "Id");
        }
    }
}
