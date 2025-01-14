using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace profile_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class NewChat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatEntityUserEntity_User_UserEntityId",
                table: "ChatEntityUserEntity");

            migrationBuilder.RenameColumn(
                name: "UserEntityId",
                table: "ChatEntityUserEntity",
                newName: "UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatEntityUserEntity_UserEntityId",
                table: "ChatEntityUserEntity",
                newName: "IX_ChatEntityUserEntity_UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatEntityUserEntity_User_UsersId",
                table: "ChatEntityUserEntity",
                column: "UsersId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatEntityUserEntity_User_UsersId",
                table: "ChatEntityUserEntity");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "ChatEntityUserEntity",
                newName: "UserEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatEntityUserEntity_UsersId",
                table: "ChatEntityUserEntity",
                newName: "IX_ChatEntityUserEntity_UserEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatEntityUserEntity_User_UserEntityId",
                table: "ChatEntityUserEntity",
                column: "UserEntityId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
