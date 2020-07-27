using Advantage.API.Models;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Advantage.API
{
    public class DataSeed
    {
        #region Private properties
        private readonly ApiContext _context;

        #endregion

        #region Constructor
        public DataSeed(ApiContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods 
        public void SeedData(int nCustomers, int nOrders)
        {
            if (!_context.Customers.Any())
            {
                SeedCustomers(nCustomers);
                _context.SaveChanges();
            }

            if (!_context.Orders.Any())
            {
                SeedOrders(nOrders);
                _context.SaveChanges();
            }

            if (!_context.Servers.Any())
            {
                SeedServers();
                _context.SaveChanges();
            }
        }

        private void SeedCustomers(int n)
        {
            List<Customer> customers = BuildCustomerList(n);

            foreach (var customer in customers)
            {
                _context.Customers.Add(customer);
            }
        }

        private void SeedOrders(int n)
        {
            List<Order> orders = BuildOrderList(n);

            foreach (var order in orders)
            {
                _context.Orders.Add(order);
            }
        }

        private void SeedServers()
        {
            List<Server> servers = BuildServerList();

            foreach(var server in servers)
            {
                _context.Servers.Add(server);
            }
        }

        private List<Customer> BuildCustomerList(int nCustomers)
        {
            var customers = new List<Customer>();
            var names = new List<string>();

            for (var i = 1; i <= nCustomers; i++)
            {
                var name = DataSeedService.MakeUniqueCustomerName(names);
                names.Add(name);

                customers.Add(new Customer
                {
                    Id = i,
                    Name = name,
                    Email = DataSeedService.MakeCustomerEmail(name),
                    State = DataSeedService.GetRandomState()
                });
            }

            return customers;
        }

        private List<Order> BuildOrderList(int nOrders)
        {
            var orders = new List<Order>();
            var rand = new Random();

            for (var i = 1; i <= nOrders; i++)
            {
                var randCustomerId = rand.Next(1, _context.Customers.Count());
                var placed = DataSeedService.GetRandomOrderPlaced();
                var completed = DataSeedService.GetRandomOrderCompleted(placed);

                orders.Add(new Order
                {
                    Id = i,
                    Customer = _context.Customers.First(c => c.Id == randCustomerId),
                    Total = DataSeedService.GetRandomOrderTotal(),
                    Placed = placed,
                    Completed = completed
                });
            }

            return orders;
        }

        private List<Server> BuildServerList()
        {
            return new List<Server>()
            {
                new Server
                {
                    Id = 1,
                    Name = "Dev-Web",
                    IsOnline = true
                },
                new Server
                {
                    Id = 2,
                    Name = "Dev-Mail",
                    IsOnline = false
                },
                new Server
                {
                    Id = 3,
                    Name = "Dev-Services",
                    IsOnline = true
                },
                new Server
                {
                    Id = 4,
                    Name = "QA-Web",
                    IsOnline = true
                },
                new Server
                {
                    Id = 5,
                    Name = "QA-Mail",
                    IsOnline = true
                },
                new Server
                {
                    Id = 6,
                    Name = "QA-Services",
                    IsOnline = true
                },
                new Server
                {
                    Id = 7,
                    Name = "Prod-Web",
                    IsOnline = true
                },
                new Server
                {
                    Id = 8,
                    Name = "Prod-Mail",
                    IsOnline = true
                },
                new Server
                {
                    Id = 9,
                    Name = "Prod-Services",
                    IsOnline = true
                },
            };
        }
        #endregion
    }
}
