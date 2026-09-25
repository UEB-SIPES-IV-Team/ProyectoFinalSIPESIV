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

        public async Task<int> Actualizar(TblEvento evento)
        {
            IEnumerable<int?> resultado = await _database.GetData<int?>("fn_tblevento_actualizar", new
            {
                p_levento_id = evento.lEvento_id,
                p_linstitucion_id = evento.lInstitucion_id,
                p_sevento_nm = evento.sEvento_nm,
                p_sanio = evento.sAnio,
                p_sgestion = evento.sGestion,
                p_sfecha_ini = evento.sFecha_ini,
                p_sfecha_fin = evento.sFecha_fin,
                p_sevento_estado = evento.sEvento_estado
            });

            return resultado.FirstOrDefault() ?? 0;
        }

        public async Task<int> Eliminar(int idEvento)
        {
            IEnumerable<int?> resultado = await _database.GetData<int?>("fn_tblevento_eliminar", new
            {
                p_levento_id = idEvento
            });

            return resultado.FirstOrDefault() ?? 0;
        }

        public async Task<TblEvento> ObtenerPorId(int idEvento)
        {
            IEnumerable<TblEvento> resultado = await _database.GetData<TblEvento>("fn_tblevento_obtenerporid", new
            {
                p_levento_id = idEvento
            });

            return resultado.FirstOrDefault();
        }

        public async Task<List<TblEvento>> ObtenerTodos()
        {
            IEnumerable<TblEvento> eventoResult = await _database.GetData<TblEvento>("fn_tblevento_obtenertodos");
            return eventoResult.ToList();
        }
    }
}