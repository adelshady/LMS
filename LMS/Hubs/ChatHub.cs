using LMS.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Hubs
{
    public class ChatHub : Hub
    {
        public void SendMessage(Message message)
        {
          //  ((IClientProxy)Clients.All).Invoke("receiveMessage", message);
        }
    }
}
