using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fr.Database.Migrations
{
    public partial class updateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedBy",
                schema: "Tree",
                table: "TreeDetail");

            migrationBuilder.DropColumn(
                name: "DeletedTime",
                schema: "Tree",
                table: "TreeDetail");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Tree",
                table: "TreeDetail");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                schema: "Plot",
                table: "PlotPoint");

            migrationBuilder.DropColumn(
                name: "DeletedTime",
                schema: "Plot",
                table: "PlotPoint");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Plot",
                table: "PlotPoint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                schema: "Tree",
                table: "TreeDetail",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedTime",
                schema: "Tree",
                table: "TreeDetail",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Tree",
                table: "TreeDetail",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                schema: "Plot",
                table: "PlotPoint",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedTime",
                schema: "Plot",
                table: "PlotPoint",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Plot",
                table: "PlotPoint",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
