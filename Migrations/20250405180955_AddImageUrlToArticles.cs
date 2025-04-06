using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Btl_web_nc.Migrations
{
    /// <inheritdoc />
    public partial class AddImageUrlToArticles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            // migrationBuilder.CreateTable(
            //     name: "sysdiagrams",
            //     columns: table => new
            //     {
            //         diagram_id = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //         name = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_unicode_ci")
            //             .Annotation("MySql:CharSet", "utf8mb4"),
            //         principal_id = table.Column<int>(type: "int", nullable: false),
            //         version = table.Column<int>(type: "int", nullable: true),
            //         definition = table.Column<byte[]>(type: "longblob", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PRIMARY", x => x.diagram_id);
            //     })
            //     .Annotation("MySql:CharSet", "utf8mb4")
            //     .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            // migrationBuilder.CreateTable(
            //     name: "topics",
            //     columns: table => new
            //     {
            //         id = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //         name = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_0900_ai_ci")
            //             .Annotation("MySql:CharSet", "utf8mb4"),
            //         description = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_0900_ai_ci")
            //             .Annotation("MySql:CharSet", "utf8mb4"),
            //         created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
            //         is_active = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValueSql: "'0'")
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PRIMARY", x => x.id);
            //     })
            //     .Annotation("MySql:CharSet", "utf8mb4")
            //     .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            // migrationBuilder.CreateTable(
            //     name: "users",
            //     columns: table => new
            //     {
            //         id = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //         email = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_0900_ai_ci")
            //             .Annotation("MySql:CharSet", "utf8mb4"),
            //         name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_0900_ai_ci")
            //             .Annotation("MySql:CharSet", "utf8mb4"),
            //         pass_word = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_0900_ai_ci")
            //             .Annotation("MySql:CharSet", "utf8mb4"),
            //         role_id = table.Column<int>(type: "int", nullable: false),
            //         is_email_confirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
            //         preferred_time = table.Column<TimeOnly>(type: "time", nullable: true, defaultValueSql: "'08:00:00'"),
            //         created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PRIMARY", x => x.id);
            //     })
            //     .Annotation("MySql:CharSet", "utf8mb4")
            //     .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            // migrationBuilder.CreateTable(
            //     name: "newsletters",
            //     columns: table => new
            //     {
            //         id = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //         topic_id = table.Column<int>(type: "int", nullable: false),
            //         name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_0900_ai_ci")
            //             .Annotation("MySql:CharSet", "utf8mb4"),
            //         description = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_0900_ai_ci")
            //             .Annotation("MySql:CharSet", "utf8mb4"),
            //         rss_url = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false, collation: "utf8mb4_0900_ai_ci")
            //             .Annotation("MySql:CharSet", "utf8mb4"),
            //         created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PRIMARY", x => x.id);
            //         table.ForeignKey(
            //             name: "newsletters_ibfk_1",
            //             column: x => x.topic_id,
            //             principalTable: "topics",
            //             principalColumn: "id",
            //             onDelete: ReferentialAction.Cascade);
            //     })
            //     .Annotation("MySql:CharSet", "utf8mb4")
            //     .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            // migrationBuilder.CreateTable(
            //     name: "articles",
            //     columns: table => new
            //     {
            //         id = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //         newsletter_id = table.Column<int>(type: "int", nullable: false),
            //         title = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, collation: "utf8mb4_0900_ai_ci")
            //             .Annotation("MySql:CharSet", "utf8mb4"),
            //         link = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_0900_ai_ci")
            //             .Annotation("MySql:CharSet", "utf8mb4"),
            //         published_at = table.Column<DateTime>(type: "datetime", nullable: true),
            //         summary = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_0900_ai_ci")
            //             .Annotation("MySql:CharSet", "utf8mb4"),
            //         created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
            //         ImageUrl = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_0900_ai_ci")
            //             .Annotation("MySql:CharSet", "utf8mb4")
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PRIMARY", x => x.id);
            //         table.ForeignKey(
            //             name: "articles_ibfk_1",
            //             column: x => x.newsletter_id,
            //             principalTable: "newsletters",
            //             principalColumn: "id",
            //             onDelete: ReferentialAction.Cascade);
            //     })
            //     .Annotation("MySql:CharSet", "utf8mb4")
            //     .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            // migrationBuilder.CreateTable(
            //     name: "email_logs",
            //     columns: table => new
            //     {
            //         id = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //         user_id = table.Column<int>(type: "int", nullable: false),
            //         newsletter_id = table.Column<int>(type: "int", nullable: false),
            //         sent_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
            //         subject = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_0900_ai_ci")
            //             .Annotation("MySql:CharSet", "utf8mb4"),
            //         status = table.Column<string>(type: "enum('success','failed')", nullable: true, defaultValueSql: "'success'", collation: "utf8mb4_0900_ai_ci")
            //             .Annotation("MySql:CharSet", "utf8mb4")
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PRIMARY", x => x.id);
            //         table.ForeignKey(
            //             name: "email_logs_ibfk_1",
            //             column: x => x.user_id,
            //             principalTable: "users",
            //             principalColumn: "id",
            //             onDelete: ReferentialAction.Cascade);
            //         table.ForeignKey(
            //             name: "email_logs_ibfk_2",
            //             column: x => x.newsletter_id,
            //             principalTable: "newsletters",
            //             principalColumn: "id",
            //             onDelete: ReferentialAction.Cascade);
            //     })
            //     .Annotation("MySql:CharSet", "utf8mb4")
            //     .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            // migrationBuilder.CreateTable(
            //     name: "subscriptions",
            //     columns: table => new
            //     {
            //         id = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //         user_id = table.Column<int>(type: "int", nullable: false),
            //         newsletter_id = table.Column<int>(type: "int", nullable: false),
            //         frequency = table.Column<string>(type: "enum('daily','weekly')", nullable: true, defaultValueSql: "'daily'", collation: "utf8mb4_0900_ai_ci")
            //             .Annotation("MySql:CharSet", "utf8mb4"),
            //         last_sent_at = table.Column<DateTime>(type: "datetime", nullable: true),
            //         created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PRIMARY", x => x.id);
            //         table.ForeignKey(
            //             name: "subscriptions_ibfk_1",
            //             column: x => x.user_id,
            //             principalTable: "users",
            //             principalColumn: "id",
            //             onDelete: ReferentialAction.Cascade);
            //         table.ForeignKey(
            //             name: "subscriptions_ibfk_2",
            //             column: x => x.newsletter_id,
            //             principalTable: "newsletters",
            //             principalColumn: "id",
            //             onDelete: ReferentialAction.Cascade);
            //     })
            //     .Annotation("MySql:CharSet", "utf8mb4")
            //     .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

        //     migrationBuilder.CreateIndex(
        //         name: "newsletter_id",
        //         table: "articles",
        //         column: "newsletter_id");

        //     migrationBuilder.CreateIndex(
        //         name: "newsletter_id1",
        //         table: "email_logs",
        //         column: "newsletter_id");

        //     migrationBuilder.CreateIndex(
        //         name: "user_id",
        //         table: "email_logs",
        //         column: "user_id");

        //     migrationBuilder.CreateIndex(
        //         name: "topic_id",
        //         table: "newsletters",
        //         column: "topic_id");

        //     migrationBuilder.CreateIndex(
        //         name: "newsletter_id2",
        //         table: "subscriptions",
        //         column: "newsletter_id");

        //     migrationBuilder.CreateIndex(
        //         name: "user_id1",
        //         table: "subscriptions",
        //         columns: new[] { "user_id", "newsletter_id" },
        //         unique: true);

        //     migrationBuilder.CreateIndex(
        //         name: "principal_id",
        //         table: "sysdiagrams",
        //         columns: new[] { "principal_id", "name" },
        //         unique: true);

        //     migrationBuilder.CreateIndex(
        //         name: "name",
        //         table: "topics",
        //         column: "name",
        //         unique: true);

        //     migrationBuilder.CreateIndex(
        //         name: "email",
        //         table: "users",
        //         column: "email",
        //         unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "articles");

            migrationBuilder.DropTable(
                name: "email_logs");

            migrationBuilder.DropTable(
                name: "subscriptions");

            migrationBuilder.DropTable(
                name: "sysdiagrams");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "newsletters");

            migrationBuilder.DropTable(
                name: "topics");
        }
    }
}
