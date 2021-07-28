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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "users");
        }

    }
}