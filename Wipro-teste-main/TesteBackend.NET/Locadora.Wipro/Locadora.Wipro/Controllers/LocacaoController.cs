using Microsoft.AspNetCore.Mvc;
using System;
using Wipro.Locadora.Models;
using Wipro.Locadora.Repositories;
using Wipro.Locadora.Repositories.Interfaces;

namespace Locadora.Wipro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class LocacaoController : ControllerBase
    {
        private ILocacaoRepository LocacaoRepository { get; set; }

        public LocacaoController()
        {
            LocacaoRepository = new LocacaoRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(LocacaoRepository.GetLocacoes());
            }
            catch (Exception exception) 
            { 
                return BadRequest(exception.Message); 
            }
        }

        [HttpGet("{idLocacao}")]
        public IActionResult GetClienteChave(int idLocacao)
        {
            try
            {
                return Ok(LocacaoRepository.GetLocacaoPorId(idLocacao));
            }
            catch (Exception exception) 
            { 
                return BadRequest(exception.Message); 
            }
        }

        [HttpPut("{idLocacao}")]
        public IActionResult Put(int idLocacao)
        {
            try
            {
                var mRetorno = LocacaoRepository.PutRealizarEntrega(idLocacao);
                return Ok(mRetorno);
            }
            catch (Exception exception) 
            { 
                return BadRequest(exception.Message); 
            }
        }

        [HttpPost]
        public IActionResult Post(Locacao locacao)
        {
            try
            {             
                string mRetorno = LocacaoRepository.Post(locacao);
                return Ok(mRetorno);
            }
            catch (Exception exception) 
            { 
                return BadRequest(exception.Message); 
            }
        }
    }
}