using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Urna.Data.Migrations
{
    /// <inheritdoc />
    public partial class UrnaMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidatos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Numero = table.Column<int>(type: "INTEGER", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Cargo = table.Column<string>(type: "TEXT", nullable: true),
                    NumVotos = table.Column<int>(type: "INTEGER", nullable: false),
                    Partido = table.Column<int>(type: "INTEGER", nullable: false),
                    FotoCandidato = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidatos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Eleitors",
                columns: table => new
                {
                    Cpf = table.Column<string>(type: "TEXT", nullable: false),
                    JaVotou = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eleitors", x => x.Cpf);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candidatos");

            migrationBuilder.DropTable(
                name: "Eleitors");
        }
    }
}
