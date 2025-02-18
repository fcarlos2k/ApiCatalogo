using APICatalogo.DTOs;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repository.Interfaces
{
    public interface IProduto
    {
        IEnumerable<ProdutoDto> Get();
        ProdutoDto Get(int id);
        ProdutoDto Post(ProdutoDto produtoDto);
        ProdutoDto Put(int id, ProdutoDto produtoDto);
        bool Delete(int id);
    }
}
