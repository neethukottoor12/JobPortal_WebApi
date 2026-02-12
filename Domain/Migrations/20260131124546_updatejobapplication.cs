using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class updatejobapplication : Migration
    {
        /// <inheritdoc />

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Remove old columns if they exist
            migrationBuilder.DropColumn(
                name: "CoverLetter",
                table: "JobApplication");

            migrationBuilder.DropColumn(
                name: "DateSubmitted",
                table: "JobApplication");

            migrationBuilder.DropColumn(
                name: "status",
                table: "JobApplication");

            // Drop old FK first
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_JobPost_JobPost_id",
                table: "JobApplication");

            // Drop old index
            migrationBuilder.DropIndex(
                name: "IX_JobApplications_JobPost_id",
                table: "JobApplication");


            migrationBuilder.DropColumn(
                name: "JobPost_id",
                table: "JobApplication");

            // Add new columns
            migrationBuilder.AddColumn<Guid>(
                name: "JobPostId",
                table: "JobApplication",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.Empty);

            migrationBuilder.AddColumn<Guid>(
                name: "JobSeekerProfileId",
                table: "JobApplication",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.Empty);

            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                table: "JobApplication",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "JobSummary",
                table: "JobApplication",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                table: "JobApplication",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.Empty);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "JobApplication",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.Empty);

            migrationBuilder.AddColumn<DateTime>(
                name: "AppliedDate",
                table: "JobApplication",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            // Recreate indexes
            migrationBuilder.CreateIndex(
                name: "IX_JobApplication_JobPostId",
                table: "JobApplication",
                column: "JobPostId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplication_JobSeekerProfileId",
                table: "JobApplication",
                column: "JobSeekerProfileId");

            // Re-add foreign keys
            migrationBuilder.AddForeignKey(
                name: "FK_JobApplication_JobPost_JobPostId",
                table: "JobApplication",
                column: "JobPostId",
                principalTable: "JobPost",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplication_JobSeekerProfile_JobSeekerProfileId",
                table: "JobApplication",
                column: "JobSeekerProfileId",
                principalTable: "JobSeekerProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop new FKs
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplication_JobPost_JobPostId",
                table: "JobApplication");

            migrationBuilder.DropForeignKey(
                name: "FK_JobApplication_JobSeekerProfile_JobSeekerProfileId",
                table: "JobApplication");

            // Drop new indexes
            migrationBuilder.DropIndex(
                name: "IX_JobApplication_JobPostId",
                table: "JobApplication");

            migrationBuilder.DropIndex(
                name: "IX_JobApplication_JobSeekerProfileId",
                table: "JobApplication");

            // Drop new columns
            migrationBuilder.DropColumn(name: "JobPostId", table: "JobApplication");
            migrationBuilder.DropColumn(name: "JobSeekerProfileId", table: "JobApplication");
            migrationBuilder.DropColumn(name: "JobTitle", table: "JobApplication");
            migrationBuilder.DropColumn(name: "JobSummary", table: "JobApplication");
            migrationBuilder.DropColumn(name: "LocationId", table: "JobApplication");
            migrationBuilder.DropColumn(name: "CompanyId", table: "JobApplication");
            migrationBuilder.DropColumn(name: "AppliedDate", table: "JobApplication");

            // Add old columns back
            migrationBuilder.AddColumn<Guid>(
                name: "JobPost_id",
                table: "JobApplication",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.Empty);

            migrationBuilder.AddColumn<string>(
                name: "CoverLetter",
                table: "JobApplication",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateSubmitted",
                table: "JobApplication",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "JobApplication",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // ⭐ Restore old index
            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_JobPost_id",
                table: "JobApplication",
                column: "JobPost_id");

            // ⭐ Restore old FK
            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_JobPost_JobPost_id",
                table: "JobApplication",
                column: "JobPost_id",
                principalTable: "JobPost",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
