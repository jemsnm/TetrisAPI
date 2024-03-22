using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TetrisAPI.Migrations
{
    /// <inheritdoc />
    public partial class updateAsForeignKeyInsteadOfPrincipalKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_games_AspNetUsers_UserId",
                table: "games");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "games",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_games_AspNetUsers_UserId",
                table: "games",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_games_AspNetUsers_UserId",
                table: "games");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "games",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AddForeignKey(
                name: "FK_games_AspNetUsers_UserId",
                table: "games",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
