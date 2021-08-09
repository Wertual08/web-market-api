using System;
using Api.Contexts;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Api.Migrations {
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210705180314_CreateRecordsTable")]
    class CreateRecordsTable : Migration {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "records",
                columns: table => new {
                    id = table.Column<long>(
                        type: "bigint primary key generated always as identity", 
                        nullable: false
                    ),
                    identifier = table.Column<Guid>(
                        type: "uuid unique", 
                        nullable: false
                    ),
                    created_at = table.Column<DateTime>(
                        type: "timestamptz", 
                        nullable: false
                    ),
                    content_type = table.Column<string>(
                        type: "varchar(255)", 
                        nullable: false
                    ),
                    name = table.Column<string>(
                        type: "varchar(255)", 
                        nullable: false
                    ),
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "records");
        }

    }
}