using System;
using Api.Contexts;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Api.Migrations {
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210705180315_InitialMigration")]
    class InitialMigration : Migration {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Role = table.Column<int>(
                        type: "integer", 
                        nullable: false
                    ),
                    Login = table.Column<string>(
                        type: "character varying(16)", 
                        maxLength: 16, 
                        nullable: false
                    ),
                    Password = table.Column<string>(
                        type: "char(128)", 
                        maxLength: 128, 
                        nullable: false
                    ),
                    Email = table.Column<string>(
                        type: "character varying(320)", 
                        maxLength: 320, 
                        nullable: false
                    ),
                    Phone = table.Column<string>(
                        type: "character varying(16)", 
                        maxLength: 16, 
                        nullable: true
                    ),
                    Name = table.Column<string>(
                        type: "character varying(128)", 
                        maxLength: 128, 
                        nullable: true
                    ),
                    Surname = table.Column<string>(
                        type: "character varying(128)", 
                        maxLength: 128, 
                        nullable: true
                    ),
                    CreatedAt = table.Column<DateTime>(
                        type: "timestamp without time zone", 
                        nullable: false
                    ),
                    UpdatedAt = table.Column<DateTime>(
                        type: "timestamp without time zone", 
                        nullable: false
                    ),
                    VerifiedAt = table.Column<DateTime>(
                        type: "timestamp without time zone", 
                        nullable: true
                    ),
                },
                constraints: table => {
                    table.PrimaryKey("PK_Users", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(
                        type: "char(1024)", 
                        maxLength: 1024, 
                        nullable: false
                    ),
                },
                constraints: table => {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Name);
                    table.ForeignKey(
                        "FK_RefreshToken_User_UserId_Id", 
                        x => x.UserId, 
                        "Users", 
                        "Id", 
                        onUpdate: ReferentialAction.Cascade, 
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

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
            migrationBuilder.DropTable(name: "Users");
            migrationBuilder.DropTable(name: "Tokens");
            migrationBuilder.DropTable(name: "Products");
        }

    }
}