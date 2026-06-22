using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Velore_Studio.Migrations
{
    /// <inheritdoc />
    public partial class AddSubjectToContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "ContactMessages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subject",
                table: "ContactMessages");
        }
    }
}
