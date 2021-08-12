using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
namespace chat.Server.Hubs
{
    public class ChatHub : Hub
    {
        private static IList<UserConnectionIdModel> _users = new List<UserConnectionIdModel>();


        public async Task SendMessage(string connectionId, string message,string userId=null)
        {
            
            await Clients.Client(connectionId).SendAsync("SendMessage", connectionId, message);
        }
        public async Task MessageReauest(string connectionId, string name,string mail,bool isRead=false)
        {
            if (_users.Where(x => x.userId != null).Any())
            {
                var data = _users.Where(x => x.userId != null).Select(x => x.ConnectionId).ToArray();
               
             await Clients.Clients(data).SendAsync("MessageReauest", connectionId, name,mail,isRead);
             await Clients.Client(connectionId).SendAsync("MessageReauest", connectionId, name,mail,isRead);

            }
            else
            {
                //Mail gönderme yapılacak
            }
           
        }

        public override Task OnConnectedAsync()
        {
            var usr = _users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (usr != null)
            {
                _users.Remove(usr);
            }
             Clients.Caller.SendAsync("ConnectionId",Context.ConnectionId);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var joinnedUser = _users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (joinnedUser != null)
            {
                string value;
                var isRemove = _users.Remove(joinnedUser);
            }
            return base.OnDisconnectedAsync(exception);
        }

        public void JoinHub(string connectionId)
        {
            var usr = _users.FirstOrDefault(x => x.ConnectionId == connectionId);
            if (usr != null)
            {
                _users.Remove(usr);
            }
            _users.Add(new UserConnectionIdModel
            {
                
                ConnectionId = Context.ConnectionId,
                

            });

        }
    }

    public class UserConnectionIdModel
    {
        public string userId { get; set; }
        public string ConnectionId { get; set; }
        public string DeviceToken { get; set; }
    }
}