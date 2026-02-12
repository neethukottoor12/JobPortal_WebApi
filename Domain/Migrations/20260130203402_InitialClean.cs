using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class InitialClean : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
                      
           

           
           

            

            migrationBuilder.CreateTable(
                name: "JobSeekerImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    JobSeekerProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSeekerImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobSeekerImage_JobSeekerProfile_JobSeekerProfileId",
                        column: x => x.JobSeekerProfileId,
                        principalTable: "JobSeekerProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfileQualification",
                columns: table => new
                {
                    JobSeekerProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QualificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileQualification", x => new { x.JobSeekerProfileId, x.QualificationId });
                    table.ForeignKey(
                        name: "FK_ProfileQualification_JobSeekerProfile_JobSeekerProfileId",
                        column: x => x.JobSeekerProfileId,
                        principalTable: "JobSeekerProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfileQualification_Qualification_QualificationId",
                        column: x => x.QualificationId,
                        principalTable: "Qualification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfileSkill",
                columns: table => new
                {
                    JobSeekerProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileSkill", x => new { x.JobSeekerProfileId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_ProfileSkill_JobSeekerProfile_JobSeekerProfileId",
                        column: x => x.JobSeekerProfileId,
                        principalTable: "JobSeekerProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfileSkill_Skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

           

           

           
           
           

          

            migrationBuilder.CreateTable(
                name: "JobSaved",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobSeekerProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobPostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__JobSaved__3214EC07C2A28AF5", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobSaved_JobPost_JobPostId",
                        column: x => x.JobPostId,
                        principalTable: "JobPost",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobSaved_JobSeekerProfile_JobSeekerProfileId",
                        column: x => x.JobSeekerProfileId,
                        principalTable: "JobSeekerProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            

         

            migrationBuilder.CreateIndex(
                name: "IX_JobSaved_JobPostId",
                table: "JobSaved",
                column: "JobPostId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSaved_JobSeekerProfileId",
                table: "JobSaved",
                column: "JobSeekerProfileId");

          

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekerImage_JobSeekerProfileId",
                table: "JobSeekerImage",
                column: "JobSeekerProfileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekerImage_JobSeekerProfileId1",
                table: "JobSeekerImage",
                column: "JobSeekerProfileId");



            migrationBuilder.CreateIndex(
                name: "IX_ProfileQualification_QualificationId",
                table: "ProfileQualification",
                column: "QualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileSkill_SkillId",
                table: "ProfileSkill",
                column: "SkillId");

           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropTable(
               name: "JobSaved");

            migrationBuilder.DropTable(
                name: "JobSeekerImage");

            migrationBuilder.DropTable(
                name: "ProfileQualification");

            migrationBuilder.DropTable(
                name: "ProfileSkill");
        }
    }
}
