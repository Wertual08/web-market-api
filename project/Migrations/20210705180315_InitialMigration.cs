using System;
using api.Contexts;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace api.Migrations {
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210705180315_InitialMigration")]
    class InitialMigration : Migration {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(
                        type: "character varying(256)", 
                        maxLength: 256, 
                        nullable: false
                    ),
                    Description = table.Column<string>(
                        type: "character varying(4096)", 
                        maxLength: 256, 
                        nullable: false
                    ),
                    CreatedAt = table.Column<DateTime>(
                        type: "timestamp without time zone", 
                        nullable: false
                    ),
                    UpdatedAt = table.Column<DateTime>(
                        type: "timestamp without time zone", 
                        nullable: false
                    ),
                },
                constraints: table => {
                    table.PrimaryKey("PK_Products", x => x.Id);
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Products");
        }

    }
}