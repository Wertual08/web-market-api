using System;
using Api.Contexts;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Api.Migrations {
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210809104729_CreateProductCategoriesTable")]
    class CreateProductCategoriesTable : Migration {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "product_categories",
                columns: table => new {
                    product_id = table.Column<long>(
                        type: "bigint references products(id)", 
                        nullable: false
                    ),
                    category_id = table.Column<long>(
                        type: "bigint references categories(id)",
                        nullable: false
                    ),
                    created_at = table.Column<DateTime>(
                        type: "timestamptz", 
                        nullable: false
                    ),
                },
                constraints: table => {
                    table.PrimaryKey(
                        name: "pk_product_categories",
                        columns: item => new { 
                            item.product_id, 
                            item.category_id,
                        }
                    );
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "product_categories");
        }

    }
}