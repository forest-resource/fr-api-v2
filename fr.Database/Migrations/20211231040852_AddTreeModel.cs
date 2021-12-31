using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fr.Database.Migrations
{
    public partial class AddTreeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Icons",
                table: "Icons");

            migrationBuilder.EnsureSchema(
                name: "Icon");

            migrationBuilder.EnsureSchema(
                name: "Tree");

            migrationBuilder.RenameTable(
                name: "Icons",
                newName: "Icon",
                newSchema: "Icon");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Icon",
                schema: "Icon",
                table: "Icon",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Tree",
                schema: "Tree",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CommonName = table.Column<string>(type: "text", nullable: false),
                    ScienceName = table.Column<string>(type: "text", nullable: true),
                    IconId = table.Column<Guid>(type: "uuid", nullable: true),
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
                    table.PrimaryKey("PK_Tree", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tree_Icon_IconId",
                        column: x => x.IconId,
                        principalSchema: "Icon",
                        principalTable: "Icon",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TreeDetail",
                schema: "Tree",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
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
                    table.PrimaryKey("PK_TreeDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TreeDetail_Tree_TreeId",
                        column: x => x.TreeId,
                        principalSchema: "Tree",
                        principalTable: "Tree",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tree_IconId",
                schema: "Tree",
                table: "Tree",
                column: "IconId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TreeDetail_TreeId",
                schema: "Tree",
                table: "TreeDetail",
                column: "TreeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TreeDetail",
                schema: "Tree");

            migrationBuilder.DropTable(
                name: "Tree",
                schema: "Tree");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Icon",
                schema: "Icon",
                table: "Icon");

            migrationBuilder.RenameTable(
                name: "Icon",
                schema: "Icon",
                newName: "Icons");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Icons",
                table: "Icons",
                column: "Id");
        }
    }
}
