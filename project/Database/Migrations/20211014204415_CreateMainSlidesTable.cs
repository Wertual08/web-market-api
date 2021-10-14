using System;
using Api.Database;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Api.Database.Migrations {
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211014204415_CreateMainSlidesTable")]
    class CreateMainSlidesTable : Migration {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "main_slides",
                columns: table => new {
                    id = table.Column<long>(
                        type: "bigint primary key generated always as identity",
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
                    updated_at = table.Column<DateTime>(
                        type: "timestamptz",
                        nullable: false
                    ),
                    name = table.Column<string>(
                        type: "text", 
                        nullable: true
                    ),
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "main_slides");
        }
    }
}