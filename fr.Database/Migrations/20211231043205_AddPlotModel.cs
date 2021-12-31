using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fr.Database.Migrations
{
    public partial class AddPlotModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Plot");

            migrationBuilder.CreateTable(
                name: "Plot",
                schema: "Plot",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PlotName = table.Column<string>(type: "text", nullable: false),
                    IsCurrent = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plot", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlotPoint",
                schema: "Plot",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    X = table.Column<double>(type: "double precision", nullable: false),
                    Y = table.Column<double>(type: "double precision", nullable: false),
                    PlotId = table.Column<Guid>(type: "uuid", nullable: false),
                    TreeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlotPoint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlotPoint_Plot_PlotId",
                        column: x => x.PlotId,
                        principalSchema: "Plot",
                        principalTable: "Plot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlotPoint_Tree_TreeId",
                        column: x => x.TreeId,
                        principalSchema: "Tree",
                        principalTable: "Tree",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plot_PlotName",
                schema: "Plot",
                table: "Plot",
                column: "PlotName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlotPoint_PlotId",
                schema: "Plot",
                table: "PlotPoint",
                column: "PlotId");

            migrationBuilder.CreateIndex(
                name: "IX_PlotPoint_TreeId",
                schema: "Plot",
                table: "PlotPoint",
                column: "TreeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlotPoint",
                schema: "Plot");

            migrationBuilder.DropTable(
                name: "Plot",
                schema: "Plot");
        }
    }
}
