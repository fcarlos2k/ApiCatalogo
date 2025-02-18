using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        var produtos = _context.Produtos.AsNoTracking().Take(10).ToList();
        return _mapper.Map<IEnumerable<ProdutoDto>>(produtos);
    }

    public ProdutoDto Get(int id)
    {
        var produto = _context.Produtos.AsNoTracking().FirstOrDefault(p => p.ProdutoId == id);
        return _mapper.Map<ProdutoDto>(produto);
    }

    public ProdutoDto Post(ProdutoDto produtoDto)
    {
        if (produtoDto is null)
            return null;

        var categoriaExiste = _context.Categorias.Any(c => c.CategoriaId == produtoDto.CategoriaId);
        if (!categoriaExiste) 
            return null;
        
        var produto = _mapper.Map<Produto>(produtoDto);
        _context.Produtos.Add(produto);
        _context.SaveChanges();

        return _mapper.Map<ProdutoDto>(produto);
    }

    public ProdutoDto Put(int id, ProdutoDto produtoDto)
    {
        var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
        if (produto == null) return null;

        _mapper.Map(produtoDto, produto);
        _context.SaveChanges();
        return _mapper.Map<ProdutoDto>(produto);
    }

    public bool Delete(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
        if (produto == null) return false;

        _context.Produtos.Remove(produto);
        _context.SaveChanges();
        return true;
    }
}
