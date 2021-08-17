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
                        type: "integer", 
                        nullable: false
                    ),
                    login = table.Column<string>(
                        type: "varchar(16) unique", 
                        nullable: false
                    ),
                    password = table.Column<string>(
                        type: "char(128)", 
                        nullable: false
                    ),
                    email = table.Column<string>(
                        type: "varchar(320) unique", 
                        nullable: false
                    ),
                    phone = table.Column<string>(
                        type: "varchar(16) unique", 
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