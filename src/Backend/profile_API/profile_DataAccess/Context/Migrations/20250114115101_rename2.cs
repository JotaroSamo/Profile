using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace profile_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class rename2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatEntityUserEntity_Chats_ChatId",
                table: "ChatEntityUserEntity");

            migrationBuilder.RenameColumn(
                name: "ChatId",
                table: "ChatEntityUserEntity",
                newName: "ChatsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatEntityUserEntity_Chats_ChatsId",
                table: "ChatEntityUserEntity",
                column: "ChatsId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatEntityUserEntity_Chats_ChatsId",
                table: "ChatEntityUserEntity");

            migrationBuilder.RenameColumn(
                name: "ChatsId",
                table: "ChatEntityUserEntity",
                newName: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatEntityUserEntity_Chats_ChatId",
                table: "ChatEntityUserEntity",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
