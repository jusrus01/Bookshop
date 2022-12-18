using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookshop.DataLayer.Migrations
{
    public partial class help2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Ratings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 18, 17, 18, 22, 643, DateTimeKind.Utc).AddTicks(746),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 18, 17, 13, 37, 158, DateTimeKind.Utc).AddTicks(345));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "OrderStates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 18, 17, 18, 22, 643, DateTimeKind.Utc).AddTicks(646),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 18, 17, 13, 37, 158, DateTimeKind.Utc).AddTicks(226));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 18, 17, 18, 22, 643, DateTimeKind.Utc).AddTicks(550),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 18, 17, 13, 37, 158, DateTimeKind.Utc).AddTicks(131));

            migrationBuilder.AlterColumn<string>(
                name: "ISBN",
                table: "Books",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Books",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 18, 17, 18, 22, 643, DateTimeKind.Utc).AddTicks(332),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 18, 17, 13, 37, 157, DateTimeKind.Utc).AddTicks(9969));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastLogin",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 18, 19, 18, 22, 643, DateTimeKind.Local).AddTicks(2222),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 18, 19, 13, 37, 158, DateTimeKind.Local).AddTicks(2181));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 18, 17, 18, 22, 643, DateTimeKind.Utc).AddTicks(267),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 18, 17, 13, 37, 157, DateTimeKind.Utc).AddTicks(9873));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 18, 17, 18, 22, 643, DateTimeKind.Utc).AddTicks(134),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 18, 17, 13, 37, 157, DateTimeKind.Utc).AddTicks(9752));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Ratings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 18, 17, 13, 37, 158, DateTimeKind.Utc).AddTicks(345),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 18, 17, 18, 22, 643, DateTimeKind.Utc).AddTicks(746));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "OrderStates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 18, 17, 13, 37, 158, DateTimeKind.Utc).AddTicks(226),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 18, 17, 18, 22, 643, DateTimeKind.Utc).AddTicks(646));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 18, 17, 13, 37, 158, DateTimeKind.Utc).AddTicks(131),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 18, 17, 18, 22, 643, DateTimeKind.Utc).AddTicks(550));

            migrationBuilder.AlterColumn<string>(
                name: "ISBN",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Books",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 18, 17, 13, 37, 157, DateTimeKind.Utc).AddTicks(9969),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 18, 17, 18, 22, 643, DateTimeKind.Utc).AddTicks(332));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastLogin",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 18, 19, 13, 37, 158, DateTimeKind.Local).AddTicks(2181),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 18, 19, 18, 22, 643, DateTimeKind.Local).AddTicks(2222));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 18, 17, 13, 37, 157, DateTimeKind.Utc).AddTicks(9873),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 18, 17, 18, 22, 643, DateTimeKind.Utc).AddTicks(267));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 18, 17, 13, 37, 157, DateTimeKind.Utc).AddTicks(9752),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 18, 17, 18, 22, 643, DateTimeKind.Utc).AddTicks(134));
        }
    }
}
