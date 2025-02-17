using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Repository.Services;

public class ProdutoService : IProduto
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ProdutoService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IEnumerable<ProdutoDto> Get()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ProdutoDto> Get(int id)
    {
        throw new NotImplementedException();
    }

    public ProdutoDto Post(ProdutoDto produtoDto)
    {
        throw new NotImplementedException();
    }

    public ProdutoDto Put(int id, ProdutoDto produtoDto)
    {
        throw new NotImplementedException();
    }

    public bool Delete(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

        if (produto == null)
        {
            return false;
        }
        _context.Categorias.Remove(produto);
        _context.SaveChanges();
        return true;
    }
}
