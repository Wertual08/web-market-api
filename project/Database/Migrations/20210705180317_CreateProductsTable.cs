using System;
using Api.Database;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Api.Database.Migrations {
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210705180317_CreateProductsTable")]
    class CreateProductsTable : Migration {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new {
                    id = table.Column<long>(
                        type: "bigint primary key generated always as identity", 
                        nullable: false
                    ),
                    old_price = table.Column<decimal>(
                        type: "numeric",
                        nullable: true
                    ),
                    price = table.Column<decimal>(
                        type: "numeric",
                        nullable: false
                    ),
                    code = table.Column<string>(
                        type: "text unique",
                        nullable: true
                    ),
                    name = table.Column<string>(
                        type: "text", 
                        nullable: false
                    ),
                    description = table.Column<string>(
                        type: "text", 
                        nullable: false
                    ),
                    private_info = table.Column<string>(
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
            migrationBuilder.DropTable(name: "products");
        }

    }
}