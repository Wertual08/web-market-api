using System;
using Api.Contexts;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Api.Migrations {
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
                    price = table.Column<decimal>(
                        type: "numeric",
                        nullable: false
                    ),
                    name = table.Column<string>(
                        type: "varchar(255)", 
                        nullable: false
                    ),
                    description = table.Column<string>(
                        type: "varchar(4095)", 
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