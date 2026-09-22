using ProyectoFinal.Datos.DataAccess;
using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Datos.Interfaces;

namespace ProyectoFinal.Datos.Repository
{
    public class TblEventoRepository : ITblEventoRepository
    {
        private readonly ProyectoFinalDatabase _database;

        public TblEventoRepository(ProyectoFinalDatabase database)
        {
            _database = database;
        }

        public async Task<int> Crear(TblEvento evento)
        {
            IEnumerable<int> resultado = await _database.GetData<int>("fn_tblevento_crear", new
            {
                linstitucion_id = evento.lInstitucion_id,
                sevento_nm = evento.sEvento_nm,
                sanio = evento.sAnio,
                sgestion = evento.sGestion,
                sfecha_ini = evento.sFecha_ini,
                sfecha_fin = evento.sFecha_fin,
                sevento_estado = evento.sEvento_estado
            });
            return resultado.FirstOrDefault();
        }

        public async Task Actualizar(TblEvento evento)
        {
            IEnumerable<int> eventoResult = await _database.GetData<int>("fn_tblevento_actualizar", new
            {
                levento_id = evento.lEvento_id,
                linstitucion_id = evento.lInstitucion_id,
                sevento_nm = evento.sEvento_nm,
                sanio = evento.sAnio,
                sgestion = evento.sGestion,
                sfecha_ini = evento.sFecha_ini,
                sfecha_fin = evento.sFecha_fin,
                sevento_estado = evento.sEvento_estado
            });
        }

        public async Task Eliminar(int idEvento)
        {
            IEnumerable<int> eventoResult = await _database.GetData<int>("fn_tblevento_eliminar", new
            {
                levento_id = idEvento
            });
        }

        public async Task<TblEvento> ObtenerPorId(int idEvento)
        {
            IEnumerable<TblEvento> eventoResult = await _database.GetData<TblEvento>("fn_tblevento_obtenerporid", new
            {
                levento_id = idEvento
            });
            return eventoResult.FirstOrDefault();
        }

        public async Task<List<TblEvento>> ObtenerTodos()
        {
            IEnumerable<TblEvento> eventoResult = await _database.GetData<TblEvento>("fn_tblevento_obtenertodos");
            return eventoResult.ToList();
        }
    }
}