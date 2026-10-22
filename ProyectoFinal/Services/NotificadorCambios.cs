
using Microsoft.AspNetCore.SignalR;
using ProyectoFinal.Hubs;

namespace ProyectoFinal.Services
{
    public class NotificadorCambios
    {
        private readonly IHubContext<CambiosHub> _hub;

        public NotificadorCambios(IHubContext<CambiosHub> hub)
        {
            _hub = hub;
        }

        public async Task NotificarAsync(string modulo)
        {
            await _hub.Clients.All.SendAsync(
                "DatosActualizados",
                modulo
            );
        }
    }
}
