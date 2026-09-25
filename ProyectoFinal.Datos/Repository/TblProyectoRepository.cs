using ProyectoFinal.Datos.DataAccess;
using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Datos.Interfaces;

namespace ProyectoFinal.Datos.Repository
{
    public class TblProyectoRepository : ITblProyectoRepository
    {
        private readonly ProyectoFinalDatabase _database;

        public TblProyectoRepository(ProyectoFinalDatabase database)
        {
            _database = database;
        }

        public async Task Crear(TblProyecto proyecto)
        {
            IEnumerable<int> proyectoResult = await _database.GetData<int>("fn_tblproyecto_crear", new
            {
                lasignatura_id = proyecto.lAsignatura_id, // integer
                sproyecto_nm = proyecto.sProyecto_nm,     // text
                sproyecto_desc = proyecto.sProyecto_desc, // text
                sproyecto_tipo = proyecto.sProyecto_tipo, // text
                sproyecto_estado = proyecto.sProyecto_estado, // boolean
                sproyecto_video = proyecto.sProyecto_video  // text
            });
        }

        public async Task Actualizar(TblProyecto entidad)
        {
            await _database.GetData<int>("fn_tblproyecto_actualizar", new
            {
                p_lproyecto_id = entidad.lProyecto_id,
                p_lasignatura_id = entidad.lAsignatura_id,
                p_sproyecto_nm = entidad.sProyecto_nm,
                p_sproyecto_desc = entidad.sProyecto_desc,
                p_sproyecto_tipo = entidad.sProyecto_tipo,
                p_sproyecto_estado = entidad.sProyecto_estado,
                p_sproyecto_video = entidad.sProyecto_video
            });
        }

        public async Task Eliminar(int idProyecto)
        {
            await _database.GetData<dynamic>("fn_tblproyecto_eliminar", new
            {
                p_lproyecto_id = idProyecto
            });
        }

        public async Task<TblProyecto> ObtenerPorId(int idProyecto)
        {
            IEnumerable<TblProyecto> proyectoResult = await _database.GetData<TblProyecto>("fn_tblproyecto_obtenerporid", new
            {
                lproyecto_id = idProyecto
            });
            return proyectoResult.FirstOrDefault();
        }

        public async Task<List<TblProyecto>> ObtenerTodos()
        {
            IEnumerable<TblProyecto> proyectoResult = await _database.GetData<TblProyecto>("fn_tblproyecto_obtenertodos");
            return proyectoResult.ToList();
        }
    }
}
