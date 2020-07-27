using Advantage.API.Models;
using Advantage.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advantage.API.Services
{
    public class ServerService : IServerService
    {
        #region Private properties
        private readonly ApiContext _context;
        #endregion

        #region Constructor
        public ServerService(ApiContext context)
        {
            _context = context;
        }

        #endregion

        #region ServerService
        public Server Get(int id)
        {
            return _context.Servers.Find(id);
        }

        public List<Server> GetAll()
        {
            return _context.Servers.OrderBy(s => s.Id).ToList();
        }

        public void Update(Server server, ServerMessage message)
        {
            if (message.Payload == "activate")
            {
                server.IsOnline = true;
            }

            if (message.Payload == "deactivate")
            {
                server.IsOnline = false;
            }

            _context.SaveChanges();
        }

        #endregion
    }
}
