using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.Project.WebUI.Migrations
{
    public partial class add__role_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "BirthDate", "BirthLocation", "CreateDate", "Department", "Email", "FirstName", "Gender", "IdentityNumber", "ImageURL", "IsActıve", "Job", "JobEnterDate", "JobExitDate", "LastName", "ModifiedDate", "Password", "Phone", "Salary", "SecondLastName", "SecondName", "UserRoleId", "YillikIzinSayisi" },
                values: new object[] { 1, "", new DateTime(2024, 8, 28, 23, 54, 0, 994, DateTimeKind.Local).AddTicks(8398), "", new DateTime(2024, 8, 28, 23, 54, 0, 994, DateTimeKind.Local).AddTicks(8399), "IT", "yeteke68@gmial.com", "Yunus Emre", 2, "1234567890", "C:\\Users\\Emre\\Downloads\\wp2013223-nissan-skyline-gt-r-r34-wallpapers.jpg", true, "DEv", new DateTime(2024, 8, 28, 23, 54, 0, 994, DateTimeKind.Local).AddTicks(8401), null, "Teke", null, "password", "", 0.0, null, null, 1, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
