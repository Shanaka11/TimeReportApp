using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server;
using server.Models;
using server.Services;
using server.Utils;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }
        // For testing
        // GET: api/Clients/Test
        [HttpGet("test")]
        public async Task<ActionResult<string>> Test()
        {
            return Ok("Tested Ok");
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            return Ok(await _clientService.Get());
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(Guid id)
        {
            var client = await _clientService.GetById(id);

            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(Guid id, Client client)
        {
            try
            {
                await _clientService.Put(id, client);
                return NoContent();
            }
            catch (Exception ex)
            {
                if(ex.Message == ExceptionConsts.NotFound)
                {
                    return NotFound();
                }
                if(ex.Message == ExceptionConsts.BadRequest)
                {
                    return BadRequest(ex.Message);
                }
                throw;
            }
        }

        // POST: api/Clients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
            var newClient = await _clientService.Post(client);
            if(newClient == null)
            {
                return NotFound();
            }
            return CreatedAtAction("GetClient", new { id = newClient.Id }, newClient);
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(Guid id)
        {
            try
            {
                await _clientService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                if(ex.Message == ExceptionConsts.NotFound)
                {
                    return NotFound();
                }
                throw;
            }
        }

    }
}
