using System;
using Api.Database;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Api.Database.Migrations {
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210705180316_CreateRefreshTokensTable")]
    class CreateRefreshTokensTable : Migration {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "refresh_tokens",
                columns: table => new {
                    user_id = table.Column<long>(
                        type: "bigint references users(id)", 
                        nullable: false
                    ),
                    name = table.Column<string>(
                        type: "text primary key", 
                        nullable: false
                    ),
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "refresh_tokens");
        }

    }
}