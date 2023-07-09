using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tuellha.Data.Migrations
{
    public partial class imagem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "FotoDB",
                table: "Publicacoes",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FotoDB",
                table: "Publicacoes");
        }
    }
}
