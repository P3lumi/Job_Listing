using Microsoft.EntityFrameworkCore.Migrations;

namespace JobListing.Data.Migrations
{
    public partial class linkingtables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IndustryId",
                table: "Job",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Job_IndustryId",
                table: "Job",
                column: "IndustryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_Industry_IndustryId",
                table: "Job",
                column: "IndustryId",
                principalTable: "Industry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_Industry_IndustryId",
                table: "Job");

            migrationBuilder.DropIndex(
                name: "IX_Job_IndustryId",
                table: "Job");

            migrationBuilder.AlterColumn<string>(
                name: "IndustryId",
                table: "Job",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
