using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class addcascadepostedby : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobPost_CompanyUser",
                table: "JobPost");

            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "SystemUser",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AddForeignKey(
                name: "FK_JobPost_CompanyUser",
                table: "JobPost",
                column: "PostedBy",
                principalTable: "CompanyUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobPost_CompanyUser",
                table: "JobPost");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "SystemUser",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_JobPost_CompanyUser",
                table: "JobPost",
                column: "PostedBy",
                principalTable: "CompanyUser",
                principalColumn: "Id");
        }
    }
}
