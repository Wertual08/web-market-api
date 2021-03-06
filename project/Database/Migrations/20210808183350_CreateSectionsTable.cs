using System;
using Api.Database;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Api.Database.Migrations {
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210808183350_CreateSectionsTable")]
    class CreateSectionsTable : Migration {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sections",
                columns: table => new {
                    id = table.Column<long>(
                        type: "bigint primary key generated always as identity", 
                        nullable: false
                    ),
                    section_id = table.Column<long>(
                        type: "bigint references sections(id)", 
                        nullable: true
                    ),
                    record_id = table.Column<long>(
                        type: "bigint references records(id)", 
                        nullable: true
                    ),
                    position = table.Column<int>(
                        type: "integer", 
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
                        nullable: false
                    ),
                },
                constraints: table => {
                    table.UniqueConstraint(
                        name: "unique_sections_name_parent_id",
                        columns: item => new { 
                            item.section_id, 
                            item.name,
                        }
                    );
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "sections");
        }

    }
}