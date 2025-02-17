using APICatalogo.DTOs;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repository.Interfaces
{
    public interface ICategoria
    {
        IEnumerable<CategoriaDto> Get();
        CategoriaDto GetById(int id);
        IEnumerable<CategoriaDto> GetPorParteDoNome(string nome);
        IEnumerable<CategoriaDto> GetCategriasComProdutos();
        CategoriaDto Add(CategoriaDto categoriaDto);
        CategoriaDto Update(int id, CategoriaDto categoriaDto);
        bool Delete(int id);
    }
}

