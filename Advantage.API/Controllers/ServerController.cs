using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advantage.API.Models;
using Advantage.API.Services.Interfaces;
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
        private readonly IServerService _serverService;

        #endregion

        #region Constructor
        public ServerController(ILogger<ServerController> logger,
            IServerService serverService)
        {
            _logger = logger;
            _serverService = serverService;
        }

        #endregion

        #region Routes
        [HttpGet]
        public IActionResult Get()
        {
            var response = _serverService.GetAll();
            return Ok(response);
        }

        [HttpGet("{id}", Name = "GetServer")]
        public IActionResult Get(int id)
        {
            var response = _serverService.Get(id);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateServerStatus(int id, [FromBody] ServerMessage message)
        {
            var server = _serverService.Get(id);

            if (server == null)
            {
                return NotFound();
            }

            _serverService.Update(server, message);

            return new NoContentResult();
        }

        #endregion
    }
}