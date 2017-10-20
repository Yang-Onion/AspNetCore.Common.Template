using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AspNetCore.Common.DBContext.Migrations.Identity
{
    public partial class addOrgandUserOrgTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserOrder",
                table: "users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "organization",
                columns: table => new
                {
                    OrgId = table.Column<int>(type: "int", nullable: false),
                    OrgName = table.Column<string>(type: "longtext", nullable: true),
                    OrgOrder = table.Column<int>(type: "int", nullable: false),
                    ParentOrgId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization", x => x.OrgId);
                });

            migrationBuilder.CreateTable(
                name: "userorganizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrgId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userorganizations", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "organization");

            migrationBuilder.DropTable(
                name: "userorganizations");

            migrationBuilder.DropColumn(
                name: "UserOrder",
                table: "users");
        }
    }
}
