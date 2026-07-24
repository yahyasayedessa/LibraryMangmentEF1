using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryMangmentEF1.Migrations
{
    /// <inheritdoc />
    public partial class AddIsAvailableColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameColumn(
                name: "IsAvailble",
                table: "Books",
                newName: "IsAvailable");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Members",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsReturned",
                table: "BorrowRecords",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "روبرت مارتن");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "أندرو تانينباوم");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PublishYear", "Title" },
                values: new object[] { 2008, "Clean Code" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PublishYear", "Title" },
                values: new object[] { 2014, "Modern Operating Systems" });

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "FullName" },
                values: new object[] { "ahmad@uni.edu", "أحمد خالد" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "IsReturned",
                table: "BorrowRecords");

            migrationBuilder.RenameColumn(
                name: "IsAvailable",
                table: "Books",
                newName: "IsAvailble");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Ahmed Ali");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Sara Mohamed");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PublishYear", "Title" },
                values: new object[] { 2020, "C# Basics" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PublishYear", "Title" },
                values: new object[] { 2022, "EF Core Guide" });

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 1,
                column: "FullName",
                value: "Student One");

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "FullName" },
                values: new object[] { 2, "Student Two" });
        }
    }
}
