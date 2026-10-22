
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ProyectoFinal.Hubs
{
    [Authorize]
    public class CambiosHub : Hub
    {
    }
}
