using Advantage.API.Database.Entities;
using Advantage.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advantage.API.Services.Interfaces
{
    public interface IServerService
    {
        Server Get(int id);

        List<Server> GetAll();

        void Update(Server server, ServerMessage message);
    }
}
