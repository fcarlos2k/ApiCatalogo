using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProduto _service;

        public ProdutosController(IProduto service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProdutoDto>> Get()
        {
            try
            {
                var produtos = _service.Get();
                if (produtos == null || !produtos.Any())
                {
                    return NotFound("Nenhum produto encontrado...");
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
                var produtoDto = _service.Get(id);
                if (produtoDto == null)
                {
                    return NotFound("Produto não encontrado...");
                }
                return Ok(produtoDto);
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
                //var categoriaExiste = _service.Categorias.Any(c => c.CategoriaId == produtoDto.CategoriaId);
                //if (produtoDto is null || !categoriaExiste)

                if (produtoDto is null)
                    return BadRequest("Dados invalidos...");

                var produtoCriado = _service.Post(produtoDto);
                return CreatedAtRoute("ObterProduto", new { id = produtoCriado.ProdutoId }, produtoCriado);
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
            try
            {
                var produtoAtualizado = _service.Put(id, produtoDto);
                if (produtoAtualizado == null)
                    return NotFound("Produto não localizado.");

                return Ok(produtoAtualizado);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
              "Ocorreu um problema ao tratar sua solicitação");
            }

        }

        [HttpDelete]
        public ActionResult<ProdutoDto> Delete(int id)
        {
            try
            {
                bool produtoRemovido = _service.Delete(id);

                if (!produtoRemovido)
                    return NotFound("Produto não localizado");

                return NoContent();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Ocorreu um problema ao tratar sua solicitação");
            }

        }
    }
}
