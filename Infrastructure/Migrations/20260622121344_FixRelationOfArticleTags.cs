using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixRelationOfArticleTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleTags_Articles_ArticlesId",
                table: "ArticleTags");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleTags_Tags_TagsId",
                table: "ArticleTags");

            migrationBuilder.RenameColumn(
                name: "TagsId",
                table: "ArticleTags",
                newName: "TagId");

            migrationBuilder.RenameColumn(
                name: "ArticlesId",
                table: "ArticleTags",
                newName: "ArticleId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleTags_TagsId",
                table: "ArticleTags",
                newName: "IX_ArticleTags_TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleTags_Articles_ArticleId",
                table: "ArticleTags",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleTags_Tags_TagId",
                table: "ArticleTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleTags_Articles_ArticleId",
                table: "ArticleTags");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleTags_Tags_TagId",
                table: "ArticleTags");

            migrationBuilder.RenameColumn(
                name: "TagId",
                table: "ArticleTags",
                newName: "TagsId");

            migrationBuilder.RenameColumn(
                name: "ArticleId",
                table: "ArticleTags",
                newName: "ArticlesId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleTags_TagId",
                table: "ArticleTags",
                newName: "IX_ArticleTags_TagsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleTags_Articles_ArticlesId",
                table: "ArticleTags",
                column: "ArticlesId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleTags_Tags_TagsId",
                table: "ArticleTags",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
