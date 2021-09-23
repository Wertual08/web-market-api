using System;
using Api.Database;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Api.Database.Migrations {
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210705180318_CreateProductRecordsTable")]
    class CreateProductRecordsTable : Migration {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "product_records",
                columns: table => new {
                    product_id = table.Column<long>(
                        type: "bigint references products(id)", 
                        nullable: false
                    ),
                    record_id = table.Column<long>(
                        type: "bigint references records(id)",
                        nullable: false
                    ),
                    created_at = table.Column<DateTime>(
                        type: "timestamptz", 
                        nullable: false
                    ),
                    position = table.Column<int>(
                        type: "integer",
                        nullable: false
                    ),
                },
                constraints: table => {
                    table.PrimaryKey(
                        name: "pk_product_records",
                        columns: item => new { 
                            item.product_id, 
                            item.record_id,
                        }
                    );
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "product_records");
        }

    }
}