using System;
using Api.Contexts;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Api.Migrations {
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210705180315_CreateUsersTabe")]
    class CreateUsersTable : Migration {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new {
                    id = table.Column<long>(
                        type: "bigint primary key generated always as identity",
                        nullable: false
                    ),
                    role = table.Column<int>(
                        type: "integer references user_roles(id)",
                        nullable: false
                    ),
                    login = table.Column<string>(
                        type: "text unique",
                        nullable: false
                    ),
                    password = table.Column<string>(
                        type: "text",
                        nullable: false
                    ),
                    email = table.Column<string>(
                        type: "text unique",
                        nullable: false
                    ),
                    phone = table.Column<string>(
                        type: "text unique",
                        nullable: true
                    ),
                    name = table.Column<string>(
                        type: "text",
                        nullable: true
                    ),
                    surname = table.Column<string>(
                        type: "text",
                        nullable: true
                    ),
                    created_at = table.Column<DateTime>(
                        type: "timestamptz",
                        nullable: false
                    ),
                    updated_at = table.Column<DateTime>(
                        type: "timestamptz",
                        nullable: false
                    ),
                    verified_at = table.Column<DateTime>(
                        type: "timestamptz",
                        nullable: true
                    ),
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "users");
        }

    }
}