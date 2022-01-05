using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fr.Database.Migrations
{
    public partial class UpdateEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ScienceName",
                schema: "Tree",
                table: "Tree",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Family",
                schema: "Tree",
                table: "Tree",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Diameter",
                schema: "Plot",
                table: "PlotPoint",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Height",
                schema: "Plot",
                table: "PlotPoint",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Sub",
                schema: "Plot",
                table: "PlotPoint",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "Plot",
                table: "Plot",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subtitle",
                schema: "Plot",
                table: "Plot",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                schema: "Plot",
                table: "Plot",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Family",
                schema: "Tree",
                table: "Tree");

            migrationBuilder.DropColumn(
                name: "Diameter",
                schema: "Plot",
                table: "PlotPoint");

            migrationBuilder.DropColumn(
                name: "Height",
                schema: "Plot",
                table: "PlotPoint");

            migrationBuilder.DropColumn(
                name: "Sub",
                schema: "Plot",
                table: "PlotPoint");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "Plot",
                table: "Plot");

            migrationBuilder.DropColumn(
                name: "Subtitle",
                schema: "Plot",
                table: "Plot");

            migrationBuilder.DropColumn(
                name: "Title",
                schema: "Plot",
                table: "Plot");

            migrationBuilder.AlterColumn<string>(
                name: "ScienceName",
                schema: "Tree",
                table: "Tree",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
