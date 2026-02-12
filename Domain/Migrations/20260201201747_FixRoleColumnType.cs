using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class FixRoleColumnType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SignUpRequest",
                table: "SignUpRequest");

            migrationBuilder.RenameTable(
                name: "SignUpRequest",
                newName: "SignUpRequests");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SignUpRequests",
                table: "SignUpRequests",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SignUpRequests",
                table: "SignUpRequests");

            migrationBuilder.RenameTable(
                name: "SignUpRequests",
                newName: "SignUpRequest");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SignUpRequest",
                table: "SignUpRequest",
                column: "Id");
        }
    }
}
