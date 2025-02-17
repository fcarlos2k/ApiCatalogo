using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repository.Services
{
    public class CategoriaService : ICategoria
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CategoriaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IEnumerable<CategoriaDto> Get()
        {
            var categorias = _context.Categorias.AsNoTracking().Take(10).ToList();
            return _mapper.Map<IEnumerable<CategoriaDto>>(categorias);
        }

        public CategoriaDto GetById(int id)
        {
            var categoria = _context.Categorias
                .AsNoTracking()
                .FirstOrDefault(c => c.CategoriaId == id);
            return _mapper.Map<CategoriaDto>(categoria);
        }


        public IEnumerable<CategoriaDto> GetPorParteDoNome(string nome)
        {
            var categorias = _context.Categorias
                .AsNoTracking()
                .Where(c => c.Nome.ToLower()
                .Contains(nome.ToLower()))
                .ToList();
            return _mapper.Map<IEnumerable<CategoriaDto>>(categorias);
        }


        public IEnumerable<CategoriaDto> GetCategriasComProdutos()
        {
            var categorias = _context.Categorias
                .AsNoTracking()
                .Include(c => c.Produtos)
                .Where(c => c.CategoriaId <= 5)
                .ToList();
            return _mapper.Map<IEnumerable<CategoriaDto>>(categorias);
        }

        public CategoriaDto Add(CategoriaDto categoriaDto)
        {
            var categoria = _mapper.Map<Categoria>(categoriaDto);
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
            return _mapper.Map<CategoriaDto>(categoria);
        }

        public CategoriaDto Update(int id, CategoriaDto categoriaDto)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
            if (categoria == null) return null;

            _mapper.Map(categoriaDto, categoria);
            _context.SaveChanges();
            return _mapper.Map<CategoriaDto>(categoria);
        }
        public bool Delete(int id)
        {
            {
                var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
                if (categoria == null) return false;

                _context.Categorias.Remove(categoria);
                _context.SaveChanges();
                return true;
            }
        }


    }
}


