using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    /// <inheritdoc />
    public partial class PopulaCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into dbo.Categorias(Nome, ImagemUrl) values ('Limpeza', '/limpeza.jpg')");
            mb.Sql("Insert into dbo.Categorias(Nome, ImagemUrl) values ('Escritorio', '/escritorio.jpg')");
            mb.Sql("Insert into dbo.Categorias(Nome, ImagemUrl) values ('Jardinagem', '/jardinagem.jpg')");
            mb.Sql("Insert into dbo.Categorias(Nome, ImagemUrl) values ('Automotivo', '/automotivo.jpg')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("delete from Categoria");
        }
    }
}
