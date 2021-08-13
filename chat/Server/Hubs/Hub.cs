using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using chat.Entites.Context;
using chat.Services;



namespace chat.Server.Hubs
{
    public class ChatHub : Hub
    {
        private static IList<UserConnectionIdModel> _users = new List<UserConnectionIdModel>();
        private readonly ChatContext _context;

        public ChatHub(ChatContext context)
        {
            _context = context;
        }

        public async Task SendMessageAgent(string connectionId, string message, string userId = null)
        {

            await Clients.Client(connectionId).SendAsync("SendMessage", connectionId, message);
        }
        public async Task SendMessageCustomer(string connectionId, string message, string userId = null)
        {
            var data = _users.Where(x => x.userId != null).Select(x => x.ConnectionId).ToArray();

            await Clients.Clients(data).SendAsync("SendMessage", connectionId, message);
        }


        public async Task MessageRequest(string connectionId, string name, string mail, string webSite, bool isAgentReady = false)
        {
            if (isAgentReady == false)
            {
                if (_users.Where(x => x.userId != null).Any())
                {
                    var data = _users.Where(x => x.userId != null).Select(x => x.ConnectionId).ToArray();
                    await Clients.Clients(data).SendAsync("MessageRequest", Context.ConnectionId, name, mail, webSite, isAgentReady);
                }
                else
                {
                    Mail.SendMail(mail, "<b>Yeni Mesaj İsteiği</b>");
                    var web = _context.WebSites.Where(x => x.WebSiteName == webSite).FirstOrDefault();
                    _context.MessageTopics.Add(new Entites.MessageTopic
                    {
                        IsRead = false,
                        RiderMail = mail,
                        RiderName = name,
                        WebSiteId = web.Id,
                        ConnectionId = Context.ConnectionId
                    });
                    _context.SaveChanges();
                }

            }

        }
        public async Task MessageResponse(string connectionId)
        {
            if (_users.Where(x => x.userId != null).Any())
            {
                var data = _users.Where(x => x.userId != null).Select(x => x.ConnectionId).ToList();
                data.Add(connectionId);
                await Clients.Client(connectionId).SendAsync("MessageRequest", connectionId, "", "", "", true);
            }

        }


        public override Task OnConnectedAsync()
        {

            var usr = _users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (usr != null)
            {
                _users.Remove(usr);
            }
            Clients.Caller.SendAsync("ConnectionId", Context.ConnectionId);

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
        public async Task JoinHub(string userId)
        {
            var usr = _users.FirstOrDefault(x => x.userId == userId);
            if (usr != null)
            {
                _users.Remove(usr);
            }
            _users.Add(new UserConnectionIdModel
            {
                userId = userId,
                ConnectionId = Context.ConnectionId,
            });

            await Clients.Caller.SendAsync("JoinHub", "Joinned");
        }
    }

    public class UserConnectionIdModel
    {
        public string userId { get; set; }
        public string ConnectionId { get; set; }
        public string DeviceToken { get; set; }
    }
}