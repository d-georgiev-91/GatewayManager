using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GatewayManager.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gateways",
                columns: table => new
                {
                    SerialNumber = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    IPv4Address = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gateways", x => x.SerialNumber);
                });

            migrationBuilder.CreateTable(
                name: "PeripheralDevice",
                columns: table => new
                {
                    Uid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vendor = table.Column<string>(maxLength: 100, nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    IsOnline = table.Column<bool>(nullable: false, defaultValue: false),
                    GatewaySerialNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeripheralDevice", x => x.Uid);
                    table.ForeignKey(
                        name: "FK_PeripheralDevice_Gateways_GatewaySerialNumber",
                        column: x => x.GatewaySerialNumber,
                        principalTable: "Gateways",
                        principalColumn: "SerialNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PeripheralDevice_GatewaySerialNumber",
                table: "PeripheralDevice",
                column: "GatewaySerialNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PeripheralDevice");

            migrationBuilder.DropTable(
                name: "Gateways");
        }
    }
}
