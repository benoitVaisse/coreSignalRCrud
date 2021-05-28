using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSignalRCrud
{
    public class SignalRServer : Hub
    {
        public async Task LoadProduct()
        {
            await Clients.All.SendAsync("LoadProducts");
        }
    }
}
