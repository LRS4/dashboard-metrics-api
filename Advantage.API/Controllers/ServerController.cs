using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advantage.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Advantage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServerController : ControllerBase
    {
        #region Private properties
        private readonly ILogger<ServerController> _logger;
        private readonly ApiContext _context;

        #endregion

        #region Constructor
        public ServerController(ILogger<ServerController> logger,
            ApiContext context)
        {
            _logger = logger;
            _context = context;
        }

        #endregion

        #region Routes
        [HttpGet]
        public IActionResult Get()
        {
            var response = _context.Servers.OrderBy(s => s.Id).ToList();
            return Ok(response);
        }

        [HttpGet("{id}", Name = "GetServer")]
        public IActionResult Get(int id)
        {
            var response = _context.Servers.Find(id);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateServerStatus(int id, [FromBody] ServerMessage message)
        {
            var server = _context.Servers.Find(id);

            if (server == null)
            {
                return NotFound();
            }

            // Refactor later: move into a service layer
            if (message.Payload == "activate")
            {
                server.IsOnline = true;
            } 

            if (message.Payload == "deactivate")
            {
                server.IsOnline = false;
            }

            _context.SaveChanges();

            return new NoContentResult();
        }

        #endregion
    }
}