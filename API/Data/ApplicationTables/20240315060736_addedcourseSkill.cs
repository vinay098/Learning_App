using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace API.Data.ApplicationTables
{
    /// <inheritdoc />
    public partial class addedcourseSkill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Batches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    BatchName = table.Column<string>(type: "longtext", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Technology = table.Column<string>(type: "longtext", nullable: true),
                    SkillId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batches", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Module_Name = table.Column<string>(type: "longtext", nullable: true),
                    Proefficiency_level = table.Column<string>(type: "longtext", nullable: true),
                    Learning_Type = table.Column<string>(type: "longtext", nullable: true),
                    Certification_Type = table.Column<string>(type: "longtext", nullable: true),
                    UserId = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Skill_Name = table.Column<string>(type: "longtext", nullable: true),
                    Skill_Family = table.Column<string>(type: "longtext", nullable: true),
                    UserId = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BatchModules",
                columns: table => new
                {
                    BatchId = table.Column<int>(type: "int", nullable: false),
                    ModuleId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchModules", x => new { x.BatchId, x.ModuleId });
                    table.ForeignKey(
                        name: "FK_BatchModules_Batches_BatchId",
                        column: x => x.BatchId,
                        principalTable: "Batches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BatchModules_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    Description = table.Column<string>(type: "longtext", nullable: true),
                    Batch_Id = table.Column<string>(type: "longtext", nullable: true),
                    BatchId = table.Column<int>(type: "int", nullable: true),
                    ModuleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Batches_BatchId",
                        column: x => x.BatchId,
                        principalTable: "Batches",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Courses_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BatchSkills",
                columns: table => new
                {
                    BatchId = table.Column<int>(type: "int", nullable: false),
                    SkillsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchSkills", x => new { x.BatchId, x.SkillsId });
                    table.ForeignKey(
                        name: "FK_BatchSkills_Batches_BatchId",
                        column: x => x.BatchId,
                        principalTable: "Batches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BatchSkills_Skills_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SkillModules",
                columns: table => new
                {
                    SkillId = table.Column<int>(type: "int", nullable: false),
                    ModuleId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillModules", x => new { x.SkillId, x.ModuleId });
                    table.ForeignKey(
                        name: "FK_SkillModules_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkillModules_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CourseSkills",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseSkills", x => new { x.CourseId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_CourseSkills_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    FirstName = table.Column<string>(type: "longtext", nullable: true),
                    LastName = table.Column<string>(type: "longtext", nullable: true),
                    Gender = table.Column<string>(type: "longtext", nullable: true),
                    Dob = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    BatchId = table.Column<int>(type: "int", nullable: true),
                    BatchModuleBatchId = table.Column<int>(type: "int", nullable: true),
                    BatchModuleModuleId = table.Column<int>(type: "int", nullable: true),
                    LearnModuleId = table.Column<int>(type: "int", nullable: true),
                    SkillModuleModuleId = table.Column<int>(type: "int", nullable: true),
                    SkillModuleSkillId = table.Column<int>(type: "int", nullable: true),
                    SkillsId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "longtext", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "longtext", nullable: true),
                    Email = table.Column<string>(type: "longtext", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "longtext", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_BatchModules_BatchModuleBatchId_BatchModuleModuleId",
                        columns: x => new { x.BatchModuleBatchId, x.BatchModuleModuleId },
                        principalTable: "BatchModules",
                        principalColumns: new[] { "BatchId", "ModuleId" });
                    table.ForeignKey(
                        name: "FK_User_Batches_BatchId",
                        column: x => x.BatchId,
                        principalTable: "Batches",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_Modules_LearnModuleId",
                        column: x => x.LearnModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_SkillModules_SkillModuleSkillId_SkillModuleModuleId",
                        columns: x => new { x.SkillModuleSkillId, x.SkillModuleModuleId },
                        principalTable: "SkillModules",
                        principalColumns: new[] { "SkillId", "ModuleId" });
                    table.ForeignKey(
                        name: "FK_User_Skills_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "Skills",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_BatchModules_ModuleId",
                table: "BatchModules",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchSkills_SkillsId",
                table: "BatchSkills",
                column: "SkillsId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_BatchId",
                table: "Courses",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ModuleId",
                table: "Courses",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSkills_SkillId",
                table: "CourseSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillModules_ModuleId",
                table: "SkillModules",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_User_BatchId",
                table: "User",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_User_BatchModuleBatchId_BatchModuleModuleId",
                table: "User",
                columns: new[] { "BatchModuleBatchId", "BatchModuleModuleId" });

            migrationBuilder.CreateIndex(
                name: "IX_User_LearnModuleId",
                table: "User",
                column: "LearnModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_User_SkillModuleSkillId_SkillModuleModuleId",
                table: "User",
                columns: new[] { "SkillModuleSkillId", "SkillModuleModuleId" });

            migrationBuilder.CreateIndex(
                name: "IX_User_SkillsId",
                table: "User",
                column: "SkillsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BatchSkills");

            migrationBuilder.DropTable(
                name: "CourseSkills");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "BatchModules");

            migrationBuilder.DropTable(
                name: "SkillModules");

            migrationBuilder.DropTable(
                name: "Batches");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "Skills");
        }
    }
}
