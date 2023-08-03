using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KUSYS_Demo.Migrations
{
    /// <inheritdoc />
    public partial class edit_baseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "StudentsCourses",
                newName: "UpdateDate");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "StudentsCourses",
                newName: "CreateDate");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Students",
                newName: "UpdateDate");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Students",
                newName: "CreateDate");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Courses",
                newName: "UpdateDate");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Courses",
                newName: "CreateDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "StudentsCourses",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "StudentsCourses",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "Students",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Students",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "Courses",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Courses",
                newName: "created_at");
        }
    }
}
