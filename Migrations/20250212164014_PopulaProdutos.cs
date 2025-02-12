using System;
using APICatalogo.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    /// <inheritdoc />
    public partial class PopulaProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("insert into dbo.Produtos(nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) values ('primeiro produto', 'descrição do produto', 10, '/primeiroproduto.jpg', 10, GETDATE (), 1)");
            mb.Sql("insert into dbo.Produtos(nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) values ('segundo produto',  'descrição do produto', 20, '/segundoproduto.jpg',  20, GETDATE (), 1)");
            mb.Sql("insert into dbo.Produtos(nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) values ('terceiro produto', 'descrição do produto', 30, '/terceiroproduto.jpg', 10, GETDATE (), 2)");
            mb.Sql("insert into dbo.Produtos(nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) values ('quarto produto',   'descrição do produto', 40, '/quartoproduto.jpg',   30, GETDATE (), 3)");
            mb.Sql("insert into dbo.Produtos(nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) values ('quinto produto',   'descrição do produto', 50, '/quintoproduto.jpg',   40, GETDATE (), 3)");
            mb.Sql("insert into dbo.Produtos(nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) values ('sexto produto',    'descrição do produto', 60, '/sextoproduto.jpg',    100,GETDATE (), 1)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("delete from dbo.Produtos");
        }
    }
}
