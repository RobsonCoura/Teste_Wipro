using Locadora.Wipro.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using Wipro.Locadora.Models;
using Wipro.Locadora.Repositories.Interfaces;

namespace Wipro.Locadora.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ClientesController : ControllerBase
    {
        private IClienteRepository ClienteRepository { get; set; }

        public ClientesController()
        {
            ClienteRepository = new ClienteRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(ClienteRepository.GetClientes());
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("{idCliente}")]
        public IActionResult GetClienteChave(int idCliente)
        {
            try
            {
                return Ok(ClienteRepository.GetClientePorId(idCliente));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        public IActionResult Post(Cliente cliente)
        {
            try
            {
                ClienteRepository.Post(cliente);
                return Ok("Cliente cadastrado");
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}