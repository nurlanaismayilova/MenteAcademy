using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sitee.Migrations
{
    /// <inheritdoc />
    public partial class newwwww : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Teams");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
