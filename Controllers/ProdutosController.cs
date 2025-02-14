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
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        private readonly IMapper _mapper;

        public ProdutosController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProdutoDto>> Get()
        {
            try
            {
                var produtos = _context.Produtos.AsNoTracking().Take(10).ToList();
                if (produtos is null)
                {
                    return NotFound("Produtos não encontrados...");
                }
                return Ok(produtos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar sua solicitação");
            }
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<IEnumerable<ProdutoDto>> Get(int id)
        {
            try
            {
                var produto = _context.Produtos.AsNoTracking().FirstOrDefault(p => p.ProdutoId == id);
                if (produto == null)
                {
                    return NotFound("Produto não encontrado...");
                }
                return Ok(produto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
            }
        }

        [HttpPost]
        public ActionResult<ProdutoDto> Post(ProdutoDto produtoDto)
        {
            try
            {
                if (produtoDto is null)
                    return BadRequest();

                var categoriaExiste = _context.Categorias.Any(c => c.CategoriaId == produtoDto.CategoriaId);
                if (!categoriaExiste)
                {
                    return BadRequest("Categoria inválida. Certifique-se de que a categoria existe.");
                }

                var produto = _mapper.Map<Produto>(produtoDto);

                _context.Produtos.Add(produto);
                _context.SaveChanges();

                var produtoRetorno = _mapper.Map<ProdutoDto>(produto);

                return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produtoRetorno);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
              "Ocorreu um problema ao tratar sua solicitação");
            }
        }

        [HttpPut]
        public ActionResult<ProdutoDto> Put(int id, ProdutoDto produtoDto)
        {
            if (id != produtoDto.ProdutoId)
            {
                return BadRequest();
            }
            _context.Entry(produtoDto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return Ok(produtoDto);

        }

        [HttpDelete]
        public ActionResult<ProdutoDto> Delete(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

            if (produto is null)
            {
                return NotFound("Produto não localizado");
            }
            _context.Produtos.Remove(produto);
            _context.SaveChanges();
            return Ok(produto);
        }


    }
}
