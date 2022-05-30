using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Veterinarios.Data.Migrations
{
    public partial class databaseyes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Donos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Donos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a", "c14afa78-6b41-4fbd-861f-393ee6e6a13c", "Administrativo", "ADMINISTRATIVO" },
                    { "c", "e0b5ad31-cda4-411d-8df0-de73a8bde046", "Cliente", "CLIENTE" },
                    { "v", "c2700446-9a6c-4b4e-a727-17ae42c97e1a", "Veterinario", "VETERINARIO" }
                });

            migrationBuilder.UpdateData(
                table: "Donos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Email",
                value: "lfreitas@gmail.com");

            migrationBuilder.UpdateData(
                table: "Donos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Email",
                value: "agomes@gmail.com");

            migrationBuilder.UpdateData(
                table: "Donos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Email",
                value: "csousa33@gmail.com");

            migrationBuilder.UpdateData(
                table: "Donos",
                keyColumn: "Id",
                keyValue: 4,
                column: "Email",
                value: "srosaa@gmail.com");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "v");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Donos");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Donos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Donos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Email",
                value: null);

            migrationBuilder.UpdateData(
                table: "Donos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Email",
                value: null);

            migrationBuilder.UpdateData(
                table: "Donos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Email",
                value: null);

            migrationBuilder.UpdateData(
                table: "Donos",
                keyColumn: "Id",
                keyValue: 4,
                column: "Email",
                value: null);
        }
    }
}
