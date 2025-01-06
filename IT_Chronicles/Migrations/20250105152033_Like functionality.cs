using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IT_Chronicles.Migrations
{
    /// <inheritdoc />
    public partial class Likefunctionality : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "blogPostLike",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlogPostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blogPostLike", x => x.Id);
                    table.ForeignKey(
                        name: "FK_blogPostLike_BlogPosts_BlogPostId",
                        column: x => x.BlogPostId,
                        principalTable: "BlogPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_blogPostLike_BlogPostId",
                table: "blogPostLike",
                column: "BlogPostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "blogPostLike");
        }
    }
}
