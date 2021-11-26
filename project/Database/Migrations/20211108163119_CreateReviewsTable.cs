using System;
using Api.Database;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Api.Database.Migrations {
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211108163119_CreateReviewsTable")]
    class CreateReviewsTable : Migration {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new {
                    id = table.Column<long>(
                        type: "bigint primary key generated always as identity", 
                        nullable: false
                    ),
                    user_id = table.Column<long>(
                        type: "bigint references users(id)",
                        nullable: true
                    ),
                    grade = table.Column<int>(
                        type: "integer",
                        nullable: true
                    ),
                    name = table.Column<string>(
                        type: "text",
                        nullable: false
                    ),
                    email = table.Column<string>(
                        type: "text",
                        nullable: true
                    ),
                    phone = table.Column<string>(
                        type: "text",
                        nullable: true
                    ),  
                    ip = table.Column<string>(
                        type: "text",
                        nullable: true
                    ),
                    description = table.Column<string>(
                        type: "text", 
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
            migrationBuilder.DropTable(name: "reviews");
        }

    }
}