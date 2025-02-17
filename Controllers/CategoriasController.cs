using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoria _service;

        public CategoriasController(ICategoria service)
        {
            _service = service;
        }



        [HttpGet]
        public ActionResult<IEnumerable<CategoriaDto>> Get()
        {
            try
            {
                var categorias = _service.Get();
                if (!categorias.Any() || categorias == null)
                {
                    return NotFound("Nenhuma categoria encontrada...");
                }
                return Ok(categorias);
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
            try
            {
                var categoriaDto = _service.GetById(id);
                if (categoriaDto == null)
                {
                    return NotFound("Nenhuma categoria encontrada...");
                }
                return Ok(categoriaDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar sua solicitação");
            }
        }

        [HttpGet("buscar/{nome}")]
        public ActionResult<IEnumerable<CategoriaDto>> GetPorParteDoNome(string nome)
        {
            var categoriasDto = _service.GetPorParteDoNome(nome);

            if (categoriasDto == null || !categoriasDto.Any())
            {
                return NotFound("Nenhuma categoria encontrada...");
            }
            return Ok(categoriasDto);
        }


        [HttpGet("ComProdutos")]
        public ActionResult<IEnumerable<CategoriaDto>> GetCategriasComProdutos()
        {
            var categoriasDto = _service.GetCategriasComProdutos();

            if (categoriasDto == null || !categoriasDto.Any())
            {
                return NotFound("Nenhuma categoria encontrada...");
            }
            return Ok(categoriasDto);
        }



        //[HttpGet("buscar/{nome}")]
        //public ActionResult<IEnumerable<CategoriaDto>> GetPorParteDoNome(string nome)
        //{
        //    var categoria = _service _context.Categorias
        //        .AsNoTracking()
        //        .Where(c => c.Nome.ToLower()
        //        .Contains(nome.ToLower()))
        //        .ToList();

        //    if (categoria == null)
        //    {
        //        return NotFound("Categoria não encontrada...");
        //    }
        //    return Ok(categoria);
        //}

        //[HttpGet("ComProdutos")]
        //public ActionResult<IEnumerable<CategoriaDto>> GetCategriasComProdutos()
        //{
        //    try
        //    {
        //        var categoriasDto = _context.Categorias
        //            .AsNoTracking()
        //            .Include(c => c.Produtos)
        //            .Where(c => c.CategoriaId <= 5)
        //            .ToList();

        //        if (categoriasDto == null || !categoriasDto.Any())
        //        {
        //            return NotFound("Nenhuma categoria encontrada...");
        //        }

        //        var categoriaDto = _mapper.Map<List<CategoriaDto>>(categorias);

        //        return Ok(categoriaDto);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            "Ocorreu um problema ao tratar sua solicitação");
        //    }
        //}


        [HttpPost]
        public ActionResult<CategoriaDto> Post(CategoriaDto categoriaDto)
        {
            if (categoriaDto == null)
                return BadRequest("Nome da categoria é obrigatório.");

            var categoriaCriada = _service.Add(categoriaDto);
            return CreatedAtRoute("ObterCategoria", new { id = categoriaCriada.CategoriaId }, categoriaCriada);
        }

        [HttpPut("{id:int}")]
        public ActionResult<CategoriaDto> Put(int id, CategoriaDto categoriaDto)
        {
            var categoriaAtualizada = _service.Update(id, categoriaDto);
            if (categoriaAtualizada == null)
                return NotFound("Categoria não encontrada.");

            return Ok(categoriaAtualizada);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            bool removido = _service.Delete(id);

            if (!removido)
                return NotFound("Categoria não localizada");

            return NoContent();
        }
    }
}
