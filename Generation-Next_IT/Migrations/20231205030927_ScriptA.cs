using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Generation_Next_IT.Migrations
{
    public partial class ScriptA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Corporate_Customer_Tbl",
                columns: table => new
                {
                    CorporateCustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CorporateCustomerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corporate_Customer_Tbl", x => x.CorporateCustomerID);
                });

            migrationBuilder.CreateTable(
                name: "Individual_Customer_Tbl",
                columns: table => new
                {
                    IndividualCustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IndividualCustomerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Individual_Customer_Tbl", x => x.IndividualCustomerID);
                });

            migrationBuilder.CreateTable(
                name: "Products_Service_Tbl",
                columns: table => new
                {
                    ProductServiceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products_Service_Tbl", x => x.ProductServiceID);
                });

            migrationBuilder.CreateTable(
                name: "Meeting_Minutes_Master_Tbl",
                columns: table => new
                {
                    MeetingMinuteMasterID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerType = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MeetingPlace = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AttendsFromClient = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    AttendsFromHost = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Agenda = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discussion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Decision = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorporateCustomerID = table.Column<int>(type: "int", nullable: true),
                    IndividualCustomerID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meeting_Minutes_Master_Tbl", x => x.MeetingMinuteMasterID);
                    table.ForeignKey(
                        name: "FK_Meeting_Minutes_Master_Tbl_Corporate_Customer_Tbl_CorporateCustomerID",
                        column: x => x.CorporateCustomerID,
                        principalTable: "Corporate_Customer_Tbl",
                        principalColumn: "CorporateCustomerID");
                    table.ForeignKey(
                        name: "FK_Meeting_Minutes_Master_Tbl_Individual_Customer_Tbl_IndividualCustomerID",
                        column: x => x.IndividualCustomerID,
                        principalTable: "Individual_Customer_Tbl",
                        principalColumn: "IndividualCustomerID");
                });

            migrationBuilder.CreateTable(
                name: "Meeting_Minutes_Details_Tbl",
                columns: table => new
                {
                    MeetingMinuteDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    MeetingMinuteMasterID = table.Column<int>(type: "int", nullable: false),
                    ProductServiceID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meeting_Minutes_Details_Tbl", x => x.MeetingMinuteDetailID);
                    table.ForeignKey(
                        name: "FK_Meeting_Minutes_Details_Tbl_Meeting_Minutes_Master_Tbl_MeetingMinuteMasterID",
                        column: x => x.MeetingMinuteMasterID,
                        principalTable: "Meeting_Minutes_Master_Tbl",
                        principalColumn: "MeetingMinuteMasterID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Meeting_Minutes_Details_Tbl_Products_Service_Tbl_ProductServiceID",
                        column: x => x.ProductServiceID,
                        principalTable: "Products_Service_Tbl",
                        principalColumn: "ProductServiceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Corporate_Customer_Tbl",
                columns: new[] { "CorporateCustomerID", "CorporateCustomerName" },
                values: new object[,]
                {
                    { 1, "Ataur" },
                    { 2, "Shakil" },
                    { 3, "Rasel" }
                });

            migrationBuilder.InsertData(
                table: "Individual_Customer_Tbl",
                columns: new[] { "IndividualCustomerID", "IndividualCustomerName" },
                values: new object[,]
                {
                    { 1, "Rakib" },
                    { 2, "Emran" },
                    { 3, "Tarek" }
                });

            migrationBuilder.InsertData(
                table: "Products_Service_Tbl",
                columns: new[] { "ProductServiceID", "ProductServiceName", "Unit" },
                values: new object[,]
                {
                    { 1, "Laptop", 5 },
                    { 2, "Website making", 10 },
                    { 3, "Mouse", 8 },
                    { 4, "Keyboard", 15 },
                    { 5, "web hosting", 12 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meeting_Minutes_Details_Tbl_MeetingMinuteMasterID",
                table: "Meeting_Minutes_Details_Tbl",
                column: "MeetingMinuteMasterID");

            migrationBuilder.CreateIndex(
                name: "IX_Meeting_Minutes_Details_Tbl_ProductServiceID",
                table: "Meeting_Minutes_Details_Tbl",
                column: "ProductServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_Meeting_Minutes_Master_Tbl_CorporateCustomerID",
                table: "Meeting_Minutes_Master_Tbl",
                column: "CorporateCustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Meeting_Minutes_Master_Tbl_IndividualCustomerID",
                table: "Meeting_Minutes_Master_Tbl",
                column: "IndividualCustomerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Meeting_Minutes_Details_Tbl");

            migrationBuilder.DropTable(
                name: "Meeting_Minutes_Master_Tbl");

            migrationBuilder.DropTable(
                name: "Products_Service_Tbl");

            migrationBuilder.DropTable(
                name: "Corporate_Customer_Tbl");

            migrationBuilder.DropTable(
                name: "Individual_Customer_Tbl");
        }
    }
}
