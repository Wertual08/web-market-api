using System;
using Api.Contexts;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Api.Migrations {
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210705180315_InitialMigration")]
    class InitialMigration : Migration {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new {
                    id = table.Column<long>(
                        type: "bigserial primary key", 
                        nullable: false
                    ),
                    role = table.Column<int>(
                        type: "integer", 
                        nullable: false
                    ),
                    login = table.Column<string>(
                        type: "varchar(16)", 
                        nullable: false
                    ),
                    password = table.Column<string>(
                        type: "char(128)", 
                        nullable: false
                    ),
                    email = table.Column<string>(
                        type: "varchar(320)", 
                        nullable: false
                    ),
                    phone = table.Column<string>(
                        type: "varchar(16)", 
                        nullable: true
                    ),
                    name = table.Column<string>(
                        type: "varchar(128)", 
                        nullable: true
                    ),
                    surname = table.Column<string>(
                        type: "varchar(128)", 
                        maxLength: 128, 
                        nullable: true
                    ),
                    created_at = table.Column<DateTime>(
                        type: "timestamp", 
                        nullable: false
                    ),
                    updated_at = table.Column<DateTime>(
                        type: "timestamp", 
                        nullable: false
                    ),
                    verified_at = table.Column<DateTime>(
                        type: "timestamp", 
                        nullable: true
                    ),
                }
            );
            migrationBuilder.CreateTable(
                name: "refresh_tokens",
                columns: table => new {
                    user_id = table.Column<long>(
                        type: "bigint references users(id)", 
                        nullable: false
                    ),
                    name = table.Column<string>(
                        type: "varchar(1024) primary key", 
                        nullable: false
                    ),
                }
            );

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new {
                    id = table.Column<long>(
                        type: "bigserial primary key", 
                        nullable: false
                    ),
                    name = table.Column<string>(
                        type: "varchar(256)", 
                        nullable: false
                    ),
                    description = table.Column<string>(
                        type: "varchar(4096)", 
                        nullable: false
                    ),
                    created_at = table.Column<DateTime>(
                        type: "timestamp", 
                        nullable: false
                    ),
                    updated_at = table.Column<DateTime>(
                        type: "timestamp", 
                        nullable: false
                    ),
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "users");
            migrationBuilder.DropTable(name: "refresh_tokens");
            migrationBuilder.DropTable(name: "products");
        }

    }
}