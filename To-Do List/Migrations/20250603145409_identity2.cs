﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace To_Do_List.Migrations
{
    /// <inheritdoc />
    public partial class identity2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoTasks_AspNetUsers_UserId",
                table: "ToDoTasks");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ToDoTasks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoTasks_AspNetUsers_UserId",
                table: "ToDoTasks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoTasks_AspNetUsers_UserId",
                table: "ToDoTasks");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ToDoTasks",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoTasks_AspNetUsers_UserId",
                table: "ToDoTasks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
