using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class RemoveResumeIdFromProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        //    migrationBuilder.DropColumn(
        //        name: "ResumeId",
        //        table: "JobSeekerProfile");
        //
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ResumeId",
                table: "JobSeekerProfile",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
