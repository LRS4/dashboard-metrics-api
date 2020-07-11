using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advantage.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Advantage.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        #region Private properties
        private readonly ILogger<CustomerController> _logger;
        private readonly ApiContext _context;

        #endregion

        #region Constructor
        public CustomerController(ILogger<CustomerController> logger,
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
            var data = _context.Customers.OrderBy(c => c.Id);

            return Ok(data);
        }

        // GET api/customers/5
        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult Get(int id)
        {
            var customer = _context.Customers.Find(id);
            return Ok(customer);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return CreatedAtRoute("GetCustomer", new { id = customer.Id }, customer);
        }

        #endregion
    }
}
