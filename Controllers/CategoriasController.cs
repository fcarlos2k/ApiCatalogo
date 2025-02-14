using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        private readonly IMapper _mapper;

        public CategoriasController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet("ComProdutos")]
        public ActionResult<IEnumerable<CategoriaDto>> GetCategriasComProdutos()
        {
            try
            {
                var categorias = _context.Categorias
                    .AsNoTracking()
                    .Include(c => c.Produtos)
                    .Where(c => c.CategoriaId <= 5)
                    .ToList();

                if (categorias == null || !categorias.Any())
                {
                    return NotFound("Nenhuma categoria encontrada...");
                }

                var categoriaDtos = _mapper.Map<List<CategoriaDto>>(categorias);

                return Ok(categoriaDtos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar sua solicitação");
            }
        }



        [HttpGet]
        public ActionResult<IEnumerable<CategoriaDto>> Get()
        {
            try
            {
                var categoriasDto = _context.Categorias.AsNoTracking().Take(10).ToList();
                if (categoriasDto is null)
                {
                    return NotFound("Categoria não encontrada...");
                }
                return Ok(categoriasDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar sua solicitação");
            }
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<CategoriaDto> Get(int id)
        {
            var categoriaDto = _context.Categorias.AsNoTracking().FirstOrDefault(c => c.CategoriaId == id);
            if (categoriaDto == null)
            {
                return NotFound("Categoria não encontrada...");
            }
            return Ok(categoriaDto);
        }


        [HttpPost]
        public ActionResult Post(CategoriaDto categoriaDto)
        {
            if (categoriaDto == null || string.IsNullOrWhiteSpace(categoriaDto.Nome))
                return BadRequest("Nome da categoria é obrigatório.");

            // Convertendo DTO para Entidade
            var categoria = new Categoria
            {
                Nome = categoriaDto.Nome,
                ImagemUrl = categoriaDto.ImagemUrl
            };

            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return CreatedAtRoute("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
        }

        [HttpPut]
        public ActionResult Put(int id, CategoriaDto categoriaDto)
        {
            if (id != categoriaDto.CategoriaId)
            {
                return BadRequest();
            }
            _context.Entry(categoriaDto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return Ok(categoriaDto);

        }

        [HttpDelete]
        public ActionResult<CategoriaDto> Delete(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);

            if (categoria is null)
            {
                return NotFound("Categoria não localizada");
            }
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
            return Ok(categoria);
        }
    }
}
