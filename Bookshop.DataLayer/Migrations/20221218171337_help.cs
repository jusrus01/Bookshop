using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookshop.DataLayer.Migrations
{
    public partial class help : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Ratings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 18, 17, 13, 37, 158, DateTimeKind.Utc).AddTicks(345),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 17, 13, 51, 32, 624, DateTimeKind.Utc).AddTicks(7848));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "OrderStates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 18, 17, 13, 37, 158, DateTimeKind.Utc).AddTicks(226),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 17, 13, 51, 32, 624, DateTimeKind.Utc).AddTicks(7707));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 18, 17, 13, 37, 158, DateTimeKind.Utc).AddTicks(131),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 17, 13, 51, 32, 624, DateTimeKind.Utc).AddTicks(7577));

            migrationBuilder.AlterColumn<string>(
                name: "ISBN",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Books",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 18, 17, 13, 37, 157, DateTimeKind.Utc).AddTicks(9969),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 17, 13, 51, 32, 624, DateTimeKind.Utc).AddTicks(7415));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastLogin",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 18, 19, 13, 37, 158, DateTimeKind.Local).AddTicks(2181),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 17, 15, 51, 32, 624, DateTimeKind.Local).AddTicks(9814));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 18, 17, 13, 37, 157, DateTimeKind.Utc).AddTicks(9873),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 17, 13, 51, 32, 624, DateTimeKind.Utc).AddTicks(7347));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 18, 17, 13, 37, 157, DateTimeKind.Utc).AddTicks(9752),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 17, 13, 51, 32, 624, DateTimeKind.Utc).AddTicks(7223));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Ratings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 17, 13, 51, 32, 624, DateTimeKind.Utc).AddTicks(7848),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 18, 17, 13, 37, 158, DateTimeKind.Utc).AddTicks(345));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "OrderStates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 17, 13, 51, 32, 624, DateTimeKind.Utc).AddTicks(7707),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 18, 17, 13, 37, 158, DateTimeKind.Utc).AddTicks(226));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 17, 13, 51, 32, 624, DateTimeKind.Utc).AddTicks(7577),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 18, 17, 13, 37, 158, DateTimeKind.Utc).AddTicks(131));

            migrationBuilder.AlterColumn<string>(
                name: "ISBN",
                table: "Books",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Books",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 17, 13, 51, 32, 624, DateTimeKind.Utc).AddTicks(7415),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 18, 17, 13, 37, 157, DateTimeKind.Utc).AddTicks(9969));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastLogin",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 17, 15, 51, 32, 624, DateTimeKind.Local).AddTicks(9814),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 18, 19, 13, 37, 158, DateTimeKind.Local).AddTicks(2181));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 17, 13, 51, 32, 624, DateTimeKind.Utc).AddTicks(7347),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 18, 17, 13, 37, 157, DateTimeKind.Utc).AddTicks(9873));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 17, 13, 51, 32, 624, DateTimeKind.Utc).AddTicks(7223),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 18, 17, 13, 37, 157, DateTimeKind.Utc).AddTicks(9752));
        }
    }
}
