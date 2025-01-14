using Microsoft.AspNetCore.SignalR;

namespace Gid.Backend.Api.Infrastructure.Context
{
    public class HubContextUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection) => connection.User?.Identity?.Name;
    }
}