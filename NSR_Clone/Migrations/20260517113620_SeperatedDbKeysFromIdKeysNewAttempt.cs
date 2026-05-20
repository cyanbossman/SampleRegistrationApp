using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NSR_Clone.Migrations
{
    /// <inheritdoc />
    public partial class SeperatedDbKeysFromIdKeysNewAttempt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    DbId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CVR = table.Column<int>(type: "INTEGER", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.DbId);
                });

            migrationBuilder.CreateTable(
                name: "Batches",
                columns: table => new
                {
                    DbId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomerDbId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batches", x => x.DbId);
                    table.ForeignKey(
                        name: "FK_Batches_Customer_CustomerDbId",
                        column: x => x.CustomerDbId,
                        principalTable: "Customer",
                        principalColumn: "DbId");
                });

            migrationBuilder.CreateTable(
                name: "Samples",
                columns: table => new
                {
                    DbId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SampleNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    BatchId = table.Column<int>(type: "INTEGER", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    BatchClassDbId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Samples", x => x.DbId);
                    table.ForeignKey(
                        name: "FK_Samples_Batches_BatchClassDbId",
                        column: x => x.BatchClassDbId,
                        principalTable: "Batches",
                        principalColumn: "DbId");
                });

            migrationBuilder.CreateTable(
                name: "Analyses",
                columns: table => new
                {
                    DbId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    IsDone = table.Column<bool>(type: "INTEGER", nullable: false),
                    SampleId = table.Column<int>(type: "INTEGER", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    SampleClassDbId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analyses", x => x.DbId);
                    table.ForeignKey(
                        name: "FK_Analyses_Samples_SampleClassDbId",
                        column: x => x.SampleClassDbId,
                        principalTable: "Samples",
                        principalColumn: "DbId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Analyses_SampleClassDbId",
                table: "Analyses",
                column: "SampleClassDbId");

            migrationBuilder.CreateIndex(
                name: "IX_Batches_CustomerDbId",
                table: "Batches",
                column: "CustomerDbId");

            migrationBuilder.CreateIndex(
                name: "IX_Samples_BatchClassDbId",
                table: "Samples",
                column: "BatchClassDbId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Analyses");

            migrationBuilder.DropTable(
                name: "Samples");

            migrationBuilder.DropTable(
                name: "Batches");

            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
