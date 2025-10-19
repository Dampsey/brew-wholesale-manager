using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedData_Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Breweries",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Abbaye de Leffe" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Abbaye de Scourmont (Chimay)" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Abbaye d'Orval" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "Abbaye de Westmalle" },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "Duvel Moortgat" },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "Brasserie de la Senne" },
                    { new Guid("77777777-7777-7777-7777-777777777777"), "Brasserie Huyghe (Delirium)" },
                    { new Guid("88888888-8888-8888-8888-888888888888"), "Brasserie Cantillon" }
                });

            migrationBuilder.InsertData(
                table: "Wholesalers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "HygieDrinks" },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "BelgoDistrib" },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), "BruxDrinks" },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), "WallonBoissons" }
                });

            migrationBuilder.InsertData(
                table: "Beers",
                columns: new[] { "Id", "AlcoholDegree", "BreweryId", "Name", "PriceHtva" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), 6.6m, new Guid("11111111-1111-1111-1111-111111111111"), "Leffe Blonde", 2.20m },
                    { new Guid("00000000-0000-0000-0000-000000000002"), 9.0m, new Guid("22222222-2222-2222-2222-222222222222"), "Chimay Bleue", 2.80m },
                    { new Guid("00000000-0000-0000-0000-000000000003"), 6.2m, new Guid("33333333-3333-3333-3333-333333333333"), "Orval", 2.60m },
                    { new Guid("00000000-0000-0000-0000-000000000004"), 9.5m, new Guid("44444444-4444-4444-4444-444444444444"), "Westmalle Triple", 3.10m },
                    { new Guid("00000000-0000-0000-0000-000000000005"), 8.5m, new Guid("55555555-5555-5555-5555-555555555555"), "Duvel", 2.90m },
                    { new Guid("00000000-0000-0000-0000-000000000006"), 6.0m, new Guid("66666666-6666-6666-6666-666666666666"), "Zinnebir", 2.40m },
                    { new Guid("00000000-0000-0000-0000-000000000007"), 8.5m, new Guid("77777777-7777-7777-7777-777777777777"), "Delirium Tremens", 3.20m },
                    { new Guid("00000000-0000-0000-0000-000000000008"), 5.0m, new Guid("88888888-8888-8888-8888-888888888888"), "Cantillon Gueuze", 4.50m },
                    { new Guid("00000000-0000-0000-0000-000000000009"), 7.0m, new Guid("66666666-6666-6666-6666-666666666666"), "Speculoos Stout", 2.70m },
                    { new Guid("00000000-0000-0000-0000-000000000010"), 5.8m, new Guid("66666666-6666-6666-6666-666666666666"), "Frites IPA", 2.30m },
                    { new Guid("00000000-0000-0000-0000-000000000011"), 6.2m, new Guid("66666666-6666-6666-6666-666666666666"), "La Blonde des Marolles", 2.10m }
                });

            migrationBuilder.InsertData(
                table: "WholesaleBeers",
                columns: new[] { "BeerId", "WholesalerId", "SalePrice", "Stock" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 2.25m, 50 },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 2.80m, 8 },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 3.25m, 2 },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 3.05m, 200 },
                    { new Guid("00000000-0000-0000-0000-000000000009"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 2.90m, 11 },
                    { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), 2.30m, 120 },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), 2.95m, 35 },
                    { new Guid("00000000-0000-0000-0000-000000000006"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), 2.55m, 40 },
                    { new Guid("00000000-0000-0000-0000-000000000008"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), 4.90m, 6 },
                    { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), 2.20m, 0 },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), 2.85m, 22 },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), 3.10m, 15 },
                    { new Guid("00000000-0000-0000-0000-000000000007"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), 3.40m, 25 },
                    { new Guid("00000000-0000-0000-0000-000000000010"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), 2.55m, 21 },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), 3.05m, 12 },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), 3.30m, 18 },
                    { new Guid("00000000-0000-0000-0000-000000000007"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), 3.50m, 10 },
                    { new Guid("00000000-0000-0000-0000-000000000010"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), 2.45m, 9 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "WholesaleBeers",
                keyColumns: new[] { "BeerId", "WholesalerId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa") });

            migrationBuilder.DeleteData(
                table: "WholesaleBeers",
                keyColumns: new[] { "BeerId", "WholesalerId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000003"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa") });

            migrationBuilder.DeleteData(
                table: "WholesaleBeers",
                keyColumns: new[] { "BeerId", "WholesalerId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000004"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa") });

            migrationBuilder.DeleteData(
                table: "WholesaleBeers",
                keyColumns: new[] { "BeerId", "WholesalerId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000005"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa") });

            migrationBuilder.DeleteData(
                table: "WholesaleBeers",
                keyColumns: new[] { "BeerId", "WholesalerId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000009"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa") });

            migrationBuilder.DeleteData(
                table: "WholesaleBeers",
                keyColumns: new[] { "BeerId", "WholesalerId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb") });

            migrationBuilder.DeleteData(
                table: "WholesaleBeers",
                keyColumns: new[] { "BeerId", "WholesalerId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb") });

            migrationBuilder.DeleteData(
                table: "WholesaleBeers",
                keyColumns: new[] { "BeerId", "WholesalerId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000006"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb") });

            migrationBuilder.DeleteData(
                table: "WholesaleBeers",
                keyColumns: new[] { "BeerId", "WholesalerId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000008"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb") });

            migrationBuilder.DeleteData(
                table: "WholesaleBeers",
                keyColumns: new[] { "BeerId", "WholesalerId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc") });

            migrationBuilder.DeleteData(
                table: "WholesaleBeers",
                keyColumns: new[] { "BeerId", "WholesalerId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000003"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc") });

            migrationBuilder.DeleteData(
                table: "WholesaleBeers",
                keyColumns: new[] { "BeerId", "WholesalerId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000005"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc") });

            migrationBuilder.DeleteData(
                table: "WholesaleBeers",
                keyColumns: new[] { "BeerId", "WholesalerId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000007"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc") });

            migrationBuilder.DeleteData(
                table: "WholesaleBeers",
                keyColumns: new[] { "BeerId", "WholesalerId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000010"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc") });

            migrationBuilder.DeleteData(
                table: "WholesaleBeers",
                keyColumns: new[] { "BeerId", "WholesalerId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") });

            migrationBuilder.DeleteData(
                table: "WholesaleBeers",
                keyColumns: new[] { "BeerId", "WholesalerId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000004"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") });

            migrationBuilder.DeleteData(
                table: "WholesaleBeers",
                keyColumns: new[] { "BeerId", "WholesalerId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000007"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") });

            migrationBuilder.DeleteData(
                table: "WholesaleBeers",
                keyColumns: new[] { "BeerId", "WholesalerId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000010"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") });

            migrationBuilder.DeleteData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "Wholesalers",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "Wholesalers",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"));

            migrationBuilder.DeleteData(
                table: "Wholesalers",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"));

            migrationBuilder.DeleteData(
                table: "Wholesalers",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"));

            migrationBuilder.DeleteData(
                table: "Breweries",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Breweries",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Breweries",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Breweries",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "Breweries",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));

            migrationBuilder.DeleteData(
                table: "Breweries",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"));

            migrationBuilder.DeleteData(
                table: "Breweries",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"));

            migrationBuilder.DeleteData(
                table: "Breweries",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"));
        }
    }
}
