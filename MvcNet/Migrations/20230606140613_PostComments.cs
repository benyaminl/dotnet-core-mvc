using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcNet.Migrations
{
    public partial class PostComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "post_comments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    insertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    publishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    postId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post_comments", x => x.id);
                    table.ForeignKey(
                        name: "FK_post_comments_posts_postId",
                        column: x => x.postId,
                        principalTable: "posts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_post_comments_postId",
                table: "post_comments",
                column: "postId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "post_comments");
        }
    }
}
