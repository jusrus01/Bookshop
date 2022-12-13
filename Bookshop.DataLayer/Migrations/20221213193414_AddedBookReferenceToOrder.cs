using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookshop.DataLayer.Migrations
{
    public partial class AddedBookReferenceToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Ratings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 13, 19, 34, 14, 224, DateTimeKind.Utc).AddTicks(1908),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 7, 23, 23, 45, 774, DateTimeKind.Utc).AddTicks(8400));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "OrderStates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 13, 19, 34, 14, 224, DateTimeKind.Utc).AddTicks(1793),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 7, 23, 23, 45, 774, DateTimeKind.Utc).AddTicks(8299));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 13, 19, 34, 14, 224, DateTimeKind.Utc).AddTicks(1631),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 7, 23, 23, 45, 774, DateTimeKind.Utc).AddTicks(8141));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Books",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 13, 19, 34, 14, 224, DateTimeKind.Utc).AddTicks(1440),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 7, 23, 23, 45, 774, DateTimeKind.Utc).AddTicks(7973));

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastLogin",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 13, 19, 34, 14, 224, DateTimeKind.Utc).AddTicks(3688),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 7, 23, 23, 45, 774, DateTimeKind.Utc).AddTicks(9900));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 13, 19, 34, 14, 224, DateTimeKind.Utc).AddTicks(1349),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 7, 23, 23, 45, 774, DateTimeKind.Utc).AddTicks(7900));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 13, 19, 34, 14, 224, DateTimeKind.Utc).AddTicks(1211),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 7, 23, 23, 45, 774, DateTimeKind.Utc).AddTicks(7780));

            migrationBuilder.CreateIndex(
                name: "IX_Books_OrderId",
                table: "Books",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Orders_OrderId",
                table: "Books",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Orders_OrderId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_OrderId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Books");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Ratings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 7, 23, 23, 45, 774, DateTimeKind.Utc).AddTicks(8400),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 13, 19, 34, 14, 224, DateTimeKind.Utc).AddTicks(1908));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "OrderStates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 7, 23, 23, 45, 774, DateTimeKind.Utc).AddTicks(8299),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 13, 19, 34, 14, 224, DateTimeKind.Utc).AddTicks(1793));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 7, 23, 23, 45, 774, DateTimeKind.Utc).AddTicks(8141),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 13, 19, 34, 14, 224, DateTimeKind.Utc).AddTicks(1631));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Books",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 7, 23, 23, 45, 774, DateTimeKind.Utc).AddTicks(7973),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 13, 19, 34, 14, 224, DateTimeKind.Utc).AddTicks(1440));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastLogin",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 7, 23, 23, 45, 774, DateTimeKind.Utc).AddTicks(9900),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 13, 19, 34, 14, 224, DateTimeKind.Utc).AddTicks(3688));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 7, 23, 23, 45, 774, DateTimeKind.Utc).AddTicks(7900),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 13, 19, 34, 14, 224, DateTimeKind.Utc).AddTicks(1349));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 7, 23, 23, 45, 774, DateTimeKind.Utc).AddTicks(7780),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 13, 19, 34, 14, 224, DateTimeKind.Utc).AddTicks(1211));
        }
    }
}
