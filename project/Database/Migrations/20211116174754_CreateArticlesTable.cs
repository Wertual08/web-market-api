using System;
using Api.Database;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Api.Database.Migrations {
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211116174754_CreateArticlesTable")]
    class CreateArticlesTable : Migration {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "articles",
                columns: table => new {
                    id = table.Column<long>(
                        type: "bigint primary key generated always as identity", 
                        nullable: false
                    ),
                    name = table.Column<string>(
                        type: "text",
                        nullable: false
                    ),
                    url = table.Column<string>(
                        type: "text",
                        nullable: true
                    ),
                    annotation = table.Column<string>(
                        type: "text",
                        nullable: true
                    ),
                    description = table.Column<string>(
                        type: "text",
                        nullable: true
                    ),
                    visible = table.Column<bool>(
                        type: "boolean",
                        nullable: false
                    ),
                    created_at = table.Column<DateTime>(
                        type: "timestamptz", 
                        nullable: false
                    ),
                    updated_at = table.Column<DateTime>(
                        type: "timestamptz", 
                        nullable: false
                    ),
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "articles");
        }

    }
}