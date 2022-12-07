using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookshop.DataLayer.Migrations
{
    public partial class addnewmigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Books");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Ratings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 7, 22, 36, 24, 740, DateTimeKind.Utc).AddTicks(474),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 6, 20, 25, 10, 435, DateTimeKind.Utc).AddTicks(8374));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "OrderStates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 7, 22, 36, 24, 740, DateTimeKind.Utc).AddTicks(358),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 6, 20, 25, 10, 435, DateTimeKind.Utc).AddTicks(8283));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 7, 22, 36, 24, 740, DateTimeKind.Utc).AddTicks(229),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 6, 20, 25, 10, 435, DateTimeKind.Utc).AddTicks(8181));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Books",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 7, 22, 36, 24, 740, DateTimeKind.Utc).AddTicks(29),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 6, 20, 25, 10, 435, DateTimeKind.Utc).AddTicks(8003));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastLogin",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 7, 22, 36, 24, 740, DateTimeKind.Utc).AddTicks(2269),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 6, 20, 25, 10, 435, DateTimeKind.Utc).AddTicks(9757));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 7, 22, 36, 24, 739, DateTimeKind.Utc).AddTicks(9890),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 6, 20, 25, 10, 435, DateTimeKind.Utc).AddTicks(7933));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 7, 22, 36, 24, 739, DateTimeKind.Utc).AddTicks(9741),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 6, 20, 25, 10, 435, DateTimeKind.Utc).AddTicks(7815));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Ratings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 6, 20, 25, 10, 435, DateTimeKind.Utc).AddTicks(8374),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 7, 22, 36, 24, 740, DateTimeKind.Utc).AddTicks(474));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "OrderStates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 6, 20, 25, 10, 435, DateTimeKind.Utc).AddTicks(8283),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 7, 22, 36, 24, 740, DateTimeKind.Utc).AddTicks(358));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 6, 20, 25, 10, 435, DateTimeKind.Utc).AddTicks(8181),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 7, 22, 36, 24, 740, DateTimeKind.Utc).AddTicks(229));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Books",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 6, 20, 25, 10, 435, DateTimeKind.Utc).AddTicks(8003),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 7, 22, 36, 24, 740, DateTimeKind.Utc).AddTicks(29));

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastLogin",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 6, 20, 25, 10, 435, DateTimeKind.Utc).AddTicks(9757),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 7, 22, 36, 24, 740, DateTimeKind.Utc).AddTicks(2269));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 6, 20, 25, 10, 435, DateTimeKind.Utc).AddTicks(7933),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 7, 22, 36, 24, 739, DateTimeKind.Utc).AddTicks(9890));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 6, 20, 25, 10, 435, DateTimeKind.Utc).AddTicks(7815),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 7, 22, 36, 24, 739, DateTimeKind.Utc).AddTicks(9741));
        }
    }
}
