﻿using System;
using Microsoft.AspNetCore.SignalR;
using MapUserMVC.Models;
using System.Threading.Tasks;

namespace MapUserMVC.Hubs
{
    public class NotificationUserHub : Hub
    {
        private readonly IUserConnectionManager _userConnectionManager;

        public NotificationUserHub(IUserConnectionManager userConnectionManager)
        {
            _userConnectionManager = userConnectionManager;
        }

        public string GetConnectionId()
        {
            var httpContext = this.Context.GetHttpContext();
            var userId = httpContext.Request.Query["userId"];

            Console.WriteLine(Context.ConnectionId);
            _userConnectionManager.KeepUserConnection(userId, Context.ConnectionId);

            return Context.ConnectionId;
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            //get the connectionId
            var connectionId = Context.ConnectionId;
            _userConnectionManager.RemoveUserConnection(connectionId);
            await Task.FromResult(0);
        }

        public async Task SendToUser(string connectionId, string message)
        {
            await Clients.Client(connectionId).SendAsync("sendToUser", message);
        }
    }
}
