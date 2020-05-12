using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubJumana.DataLayer.Migrations
{
    public partial class start1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_Users_UserSendInvitationId",
                table: "Invitations");

            migrationBuilder.DropIndex(
                name: "IX_Invitations_UserSendInvitationId",
                table: "Invitations");

            migrationBuilder.DropIndex(
                name: "IX_Invitations_UserSendInvitation_fk",
                table: "Invitations");

            migrationBuilder.DropColumn(
                name: "UserSendInvitationId",
                table: "Invitations");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_UserRegisterWithInvitation_fk",
                table: "Invitations",
                column: "UserRegisterWithInvitation_fk",
                unique: true,
                filter: "[UserRegisterWithInvitation_fk] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_UserSendInvitation_fk",
                table: "Invitations",
                column: "UserSendInvitation_fk");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_Users_UserRegisterWithInvitation_fk",
                table: "Invitations",
                column: "UserRegisterWithInvitation_fk",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_Users_UserRegisterWithInvitation_fk",
                table: "Invitations");

            migrationBuilder.DropIndex(
                name: "IX_Invitations_UserRegisterWithInvitation_fk",
                table: "Invitations");

            migrationBuilder.DropIndex(
                name: "IX_Invitations_UserSendInvitation_fk",
                table: "Invitations");

            migrationBuilder.AddColumn<int>(
                name: "UserSendInvitationId",
                table: "Invitations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_UserSendInvitationId",
                table: "Invitations",
                column: "UserSendInvitationId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_UserSendInvitation_fk",
                table: "Invitations",
                column: "UserSendInvitation_fk",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_Users_UserSendInvitationId",
                table: "Invitations",
                column: "UserSendInvitationId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
