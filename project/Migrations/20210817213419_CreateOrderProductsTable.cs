using System;
using Api.Contexts;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Api.Migrations {
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210817213419_CreateOrderProductsTable")]
    class CreateOrderProductsTable : Migration {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "order_products",
                columns: table => new {
                    order_id = table.Column<long>(
                        type: "bigint references orders(id)",
                        nullable: false
                    ),
                    product_id = table.Column<long>(
                        type: "bigint references products(id)", 
                        nullable: false
                    ),
                    amount = table.Column<int>(
                        type: "integer", 
                        nullable: false
                    ),
                    created_at = table.Column<DateTime>(
                        type: "timestamptz", 
                        nullable: false
                    ),
                },
                constraints: table => {
                    table.PrimaryKey(
                        name: "pk_order_products",
                        columns: item => new { 
                            item.order_id,
                            item.product_id, 
                        }
                    );
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "order_products");
        }
    }
}