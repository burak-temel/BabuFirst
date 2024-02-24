using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Migrations.Pg
{
    /// <inheritdoc />
    public partial class AddCustomerAndInvoiceTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Translates",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Translates",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Translates",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Translates",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Translates",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "OperationClaims",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "OperationClaims",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "OperationClaims",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "OperationClaims",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "OperationClaims",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "MobileLogins",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "MobileLogins",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MobileLogins",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "MobileLogins",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "MobileLogins",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Languages",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Languages",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Languages",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Languages",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Languages",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Groups",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Groups",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Groups",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Groups",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Groups",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Rate = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxRates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrganizationId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customer_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrganizationId = table.Column<int>(type: "integer", nullable: false),
                    Salary = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    TaxRateId = table.Column<int>(type: "integer", nullable: false),
                    TaxRateId1 = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_TaxRates_TaxRateId",
                        column: x => x.TaxRateId,
                        principalTable: "TaxRates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_TaxRates_TaxRateId1",
                        column: x => x.TaxRateId1,
                        principalTable: "TaxRates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LicensePlate = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Make = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Model = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    VIN = table.Column<string>(type: "character varying(17)", maxLength: 17, nullable: false),
                    Mileage = table.Column<double>(type: "double precision", nullable: false),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ServiceDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    LaborCost = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    VehicleId = table.Column<int>(type: "integer", nullable: false),
                    InvoiceId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceRecords_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceRecords_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceItems",
                columns: table => new
                {
                    ServiceRecordId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceItems", x => new { x.ServiceRecordId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ServiceItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceItems_ServiceRecords_ServiceRecordId",
                        column: x => x.ServiceRecordId,
                        principalTable: "ServiceRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 121,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 122,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 123,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 124,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 125,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 126,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 127,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 128,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 129,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 130,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 131,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 132,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 133,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 134,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 135,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 136,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 137,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 138,
                columns: new[] { "CreatedAt", "CreatedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_OrganizationId",
                table: "Customer",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_OrganizationId",
                table: "Employee",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CustomerId",
                table: "Invoices",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_TaxRateId",
                table: "Products",
                column: "TaxRateId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_TaxRateId1",
                table: "Products",
                column: "TaxRateId1");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceItems_ProductId",
                table: "ServiceItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRecords_InvoiceId",
                table: "ServiceRecords",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRecords_VehicleId",
                table: "ServiceRecords",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_CustomerId",
                table: "Vehicles",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "ServiceItems");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ServiceRecords");

            migrationBuilder.DropTable(
                name: "TaxRates");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Translates");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Translates");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Translates");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Translates");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Translates");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "OperationClaims");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "OperationClaims");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "OperationClaims");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "OperationClaims");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "OperationClaims");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "MobileLogins");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "MobileLogins");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MobileLogins");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "MobileLogins");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "MobileLogins");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Groups");
        }
    }
}
