using System;
using Api.Contexts;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations {
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210817151525_CreateOrdersTable")]
    class CreateOrdersTable : Migration {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new {
                    id = table.Column<long>(
                        type: "bigint primary key generated always as identity",
                        nullable: false
                    ),
                    state = table.Column<int>(
                        type: "integer references order_states(id)",
                        nullable: true
                    ),
                    user_id = table.Column<long>(
                        type: "bigint references users(id)",
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
                    requested_at = table.Column<DateTime>(
                        type: "timestamptz",
                        nullable: true
                    ),
                    finished_at = table.Column<DateTime>(
                        type: "timestamptz",
                        nullable: true
                    ),
                    email = table.Column<string>(
                        type: "text",
                        nullable: false
                    ),
                    phone = table.Column<string>(
                        type: "text",
                        nullable: true
                    ),
                    name = table.Column<string>(
                        type: "text",
                        nullable: false
                    ),
                    surname = table.Column<string>(
                        type: "text",
                        nullable: false
                    ),
                    address = table.Column<string>(
                        type: "text",
                        nullable: true
                    ),
                    promo_code = table.Column<string>(
                        type: "text",
                        nullable: true
                    ),
                    description = table.Column<string>(
                        type: "text",
                        nullable: true
                    ),
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "orders");
        }

    }
}