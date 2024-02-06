using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace stoneXXI.Migrations
{
    /// <inheritdoc />
    public partial class InitMagration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var dep = "Departments";
            if (!DbExt.TableExists(dep))
            {
                migrationBuilder.CreateTable(
                    name: dep,
                    columns: table => new
                    {
                        Id = table.Column<int>(type: "integer", nullable: false)
                            .Annotation("Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                        Name = table.Column<string>(type: "text", nullable: false)
                    },
                    constraints: table => { table.PrimaryKey("PK_Departments", x => x.Id); });

            }

            var hrs = "Hrs";
            if (!DbExt.TableExists(hrs))
                migrationBuilder.CreateTable(
                name: hrs,
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hrs", x => x.Id);
                });

            var vacs = "Vacancies";
            if (!DbExt.TableExists(vacs))
                migrationBuilder.CreateTable(
                name: vacs,
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    DepartmentId = table.Column<int>(type: "integer", nullable: false),
                    TestTask = table.Column<string>(type: "text", nullable: true),
                    HrSpecialistId = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Link = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacancies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vacancies_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vacancies_Hrs_HrSpecialistId",
                        column: x => x.HrSpecialistId,
                        principalTable: "Hrs",
                        principalColumn: "Id");
                });

            var cands = "Candidates";
            if (!DbExt.TableExists(cands))
                migrationBuilder.CreateTable(
                name: cands,
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HrId = table.Column<int>(type: "integer", nullable: true),
                    VacancyId = table.Column<int>(type: "integer", nullable: false),
                    CurrentStage = table.Column<int>(type: "integer", nullable: false),
                    IsNeedTestTask = table.Column<bool>(type: "boolean", nullable: false),
                    IsPassedProbationaryPeriod = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candidates_Hrs_HrId",
                        column: x => x.HrId,
                        principalTable: "Hrs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Candidates_Vacancies_VacancyId",
                        column: x => x.VacancyId,
                        principalTable: "Vacancies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            var canHrIndex = "IX_Candidates_HrId";
            if (!DbExt.IndexExists(cands, canHrIndex))
                migrationBuilder.CreateIndex(
                name: canHrIndex,
                table: cands,
                column: "HrId");

            var canVacIndex = "IX_Candidates_VacancyId";
            if (!DbExt.IndexExists(cands, canVacIndex))
                migrationBuilder.CreateIndex(
                name: canVacIndex,
                table: cands,
                column: "VacancyId");

            var vacDepIndex = "IX_Vacancies_DepartmentId";
            if (!DbExt.IndexExists(vacs, vacDepIndex))
                migrationBuilder.CreateIndex(
                name: vacDepIndex,
                table: vacs,
                column: "DepartmentId");

            var vacHrIndex = "IX_Vacancies_HrSpecialistId";
            if (!DbExt.IndexExists(vacs, vacHrIndex))
                migrationBuilder.CreateIndex(
                name: vacHrIndex,
                table: vacs,
                column: "HrSpecialistId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "Vacancies");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Hrs");
        }
    }
}
