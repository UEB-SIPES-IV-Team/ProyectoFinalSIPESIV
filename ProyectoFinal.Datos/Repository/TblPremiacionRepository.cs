using ProyectoFinal.Datos.DataAccess;
using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Datos.Interfaces;

namespace ProyectoFinal.Datos.Repository
{
    public class TblPremiacionRepository : ITblPremiacionRepository
    {
        private readonly ProyectoFinalDatabase _database;

        public TblPremiacionRepository(ProyectoFinalDatabase database)
        {
            _database = database;
        }

        public async Task<int> Crear(TblPremiacion premiacion)
        {
            IEnumerable<int> resultado = await _database.GetData<int>("fn_tblpremiacion_crear", new
            {
                lpremiacion_id = premiacion.lPremiacion_id,
                lproyecto_id = premiacion.lProyecto_id,
                levento_id = premiacion.lEvento_id,
                spremiacion_tipo = premiacion.sPremiacion_tipo,
                sposicion = Convert.ToInt32(premiacion.sPosicion)
            });
            return resultado.FirstOrDefault();
        }

        public async Task Actualizar(TblPremiacion entidad)
        {
            await _database.GetData<int?>("fn_tblpremiacion_actualizar", new
            {
                p_lpremiacion_id = entidad.lPremiacion_id,
                p_lproyecto_id = entidad.lProyecto_id,
                p_levento_id = entidad.lEvento_id,
                p_spremiacion_tipo = entidad.sPremiacion_tipo,
                p_sposicion = entidad.sPosicion 
            });
        }

        public async Task Eliminar(int idPremiacion)
        {
            await _database.GetData<dynamic>("fn_tblpremiacion_eliminar", new
            {
                p_lpremiacion_id = idPremiacion
            });
        }

        public async Task<TblPremiacion> ObtenerPorId(int idPremiacion)
        {
            IEnumerable<TblPremiacion> premiacionResult = await _database.GetData<TblPremiacion>("fn_tblpremiacion_obtenerporid", new
            {
                lpremiacion_id = idPremiacion
            });

            return premiacionResult.FirstOrDefault();
        }

        public async Task<List<TblPremiacion>> ObtenerTodos()
        {
            IEnumerable<TblPremiacion> premiacionResult = await _database.GetData<TblPremiacion>("fn_tblpremiacion_obtenertodos");
            return premiacionResult.ToList();
        }
    }
}