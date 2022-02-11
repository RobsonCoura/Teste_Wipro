using Locadora.Wipro.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using Wipro.Locadora.Models;
using Wipro.Locadora.Repositories.Interfaces;

namespace Locadora.Wipro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class FilmesController : ControllerBase
    {
        private IFilmeRepository FilmeRepository { get; set; }

        public FilmesController()
        {
            FilmeRepository = new FilmeRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(FilmeRepository.GetFilmes());
            }
            catch (Exception exception) 
            { 
                return BadRequest(exception.Message); 
            }
        }

        [HttpGet("{idFilme}")]
        public IActionResult GetClienteChave(int idFilme)
        {
            try
            {
                return Ok(FilmeRepository.GetFilmePorId(idFilme));
            }
            catch (Exception exception) 
            { 
                return BadRequest(exception.Message); 
            }
        }

        [HttpPost]
        public IActionResult Post(Filme filme)
        {
            try
            {
                FilmeRepository.Post(filme);
                return Ok("Cadastrado.");
            }
            catch (Exception exception) 
            { 
                return BadRequest(exception.Message); 
            }
        }

        [HttpPut("{idFilme}")]
        public IActionResult Put (Filme filme)
        {
            try
            {
                FilmeRepository.Put(filme);
                return Ok("Sucesso ao alterar.");
            }
            catch (Exception exception) 
            { 
                return BadRequest(exception.Message); 
            }
        }
    }
}