using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace profile_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class renameColums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatEntityUserEntity_Chats_ChatEntitiesId",
                table: "ChatEntityUserEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_User_UserId1",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "Messages",
                newName: "UserEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_UserId1",
                table: "Messages",
                newName: "IX_Messages_UserEntityId");

            migrationBuilder.RenameColumn(
                name: "ChatEntitiesId",
                table: "ChatEntityUserEntity",
                newName: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatEntityUserEntity_Chats_ChatId",
                table: "ChatEntityUserEntity",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_User_UserEntityId",
                table: "Messages",
                column: "UserEntityId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatEntityUserEntity_Chats_ChatId",
                table: "ChatEntityUserEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_User_UserEntityId",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "UserEntityId",
                table: "Messages",
                newName: "UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_UserEntityId",
                table: "Messages",
                newName: "IX_Messages_UserId1");

            migrationBuilder.RenameColumn(
                name: "ChatId",
                table: "ChatEntityUserEntity",
                newName: "ChatEntitiesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatEntityUserEntity_Chats_ChatEntitiesId",
                table: "ChatEntityUserEntity",
                column: "ChatEntitiesId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_User_UserId1",
                table: "Messages",
                column: "UserId1",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
