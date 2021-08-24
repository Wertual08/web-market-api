using System;
using Api.Database;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Api.Database.Migrations {
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210822105016_CreateCartProductsTable")]
    class CreateCartProductsTable : Migration {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cart_products",
                columns: table => new {
                    user_id = table.Column<long>(
                        type: "bigint references users(id)",
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
                        name: "pk_user_products",
                        columns: item => new { 
                            item.user_id,
                            item.product_id, 
                        }
                    );
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "user_products");
        }
    }
}