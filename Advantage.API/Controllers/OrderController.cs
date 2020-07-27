using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advantage.API.Database.Entities;
using Advantage.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Advantage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        #region Private properties
        private readonly ILogger<OrderController> _logger;
        private readonly ApiContext _context;

        #endregion

        #region Constructor
        public OrderController(ILogger<OrderController> logger,
            ApiContext context)
        {
            _logger = logger;
            _context = context;
        }

        #endregion

        #region Routes
        // GET api/order/pageNumber/pageSize
        [HttpGet("{pageIndex:int}/{pageSize:int}")]
        public IActionResult Get(int pageIndex, int pageSize)
        {
            var data = _context.Orders
                .Include(o => o.Customer)
                .OrderByDescending(c => c.Placed);

            var page = new PaginatedResponse<Order>(data, pageIndex, pageSize);

            var totalCount = data.Count();
            var totalPages = Math.Ceiling((double)totalCount / pageSize);

            var response = new
            {
                Page = page,
                TotalPages = totalPages
            };

            return Ok(response);
        }

        [HttpGet("ByState")]
        public IActionResult ByState()
        {
            var orders = _context.Orders.Include(o => o.Customer).ToList();

            // By including customer we can aggregate orders by state
            var groupedResult = orders.GroupBy(o => o.Customer.State)
                .ToList()
                .Select(group => new
                {
                    State = group.Key,
                    Total = group.Sum(x => x.Total)
                })
                .OrderByDescending(res => res.Total)
                .ToList();

            return Ok(groupedResult);
        }

        [HttpGet("ByCustomer/{numberOfCustomers}")]
        public IActionResult ByCustomer(int numberOfCustomers)
        {
            var orders = _context.Orders.Include(o => o.Customer).ToList();

            // By including customer we can aggregate orders
            var groupedResult = orders.GroupBy(o => o.Customer.Id)
                .ToList()
                .Select(group => new
                {
                    Name = _context.Customers.Find(group.Key).Name,
                    Total = group.Sum(x => x.Total)
                })
                .OrderByDescending(res => res.Total)
                .Take(numberOfCustomers)
                .ToList();

            return Ok(groupedResult);
        }

        [HttpGet("GetOrder/{id}", Name = "GetOrder")]
        public IActionResult GetOrder(int id)
        {
            var order =_context.Orders.Include(o => o.Customer).FirstOrDefault(o => o.Id == id);
            return Ok(order);
        }

        #endregion
    }
}