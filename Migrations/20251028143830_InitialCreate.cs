using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jewellery_vertification_project.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jewellery",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompanyCode = table.Column<string>(type: "TEXT", nullable: false),
                    BagNo = table.Column<string>(type: "TEXT", nullable: false),
                    OrderNo = table.Column<string>(type: "TEXT", nullable: false),
                    DesignNo = table.Column<string>(type: "TEXT", nullable: false),
                    BagQN = table.Column<int>(type: "INTEGER", nullable: false),
                    Customer = table.Column<string>(type: "TEXT", nullable: false),
                    CertificateType = table.Column<string>(type: "TEXT", nullable: false),
                    CertificateNumber = table.Column<string>(type: "TEXT", nullable: false),
                    CertificateEnterDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    GrossWeight = table.Column<decimal>(type: "TEXT", nullable: false),
                    DocNumber = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jewellery", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jewellery");
        }
    }
}
