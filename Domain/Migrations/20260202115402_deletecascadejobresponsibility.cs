using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class deletecascadejobresponsibility : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobResponsibility_JobPost",
                table: "JobResponsibility");

            migrationBuilder.AddForeignKey(
                name: "FK_JobResponsibility_JobPost",
                table: "JobResponsibility",
                column: "JobPost",
                principalTable: "JobPost",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobResponsibility_JobPost",
                table: "JobResponsibility");

            migrationBuilder.AddForeignKey(
                name: "FK_JobResponsibility_JobPost",
                table: "JobResponsibility",
                column: "JobPost",
                principalTable: "JobPost",
                principalColumn: "Id");
        }
    }
}
