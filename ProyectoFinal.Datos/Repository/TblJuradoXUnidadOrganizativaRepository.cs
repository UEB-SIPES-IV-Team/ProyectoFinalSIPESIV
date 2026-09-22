using ProyectoFinal.Datos.DataAccess;
using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Datos.Interfaces;

namespace ProyectoFinal.Datos.Repository
{
    public class TblJuradoXUnidadOrganizativaRepository : ITblJuradoXUnidadOrganizativaRepository
    {
        private readonly ProyectoFinalDatabase _database;

        public TblJuradoXUnidadOrganizativaRepository(ProyectoFinalDatabase database)
        {
            _database = database; 
        }

        public async Task Crear(TblJuradoXUnidadOrganizativa juradoXUnidad)
        {
            IEnumerable<int> result = await _database.GetData<int>("fn_tbljuradoxunidadorganizativa_crear", new
            {
                ljurado_id = juradoXUnidad.lJurado_id,
                luniversidadorganizativa_id = juradoXUnidad.lUniversidadOrganizativa_id
            });
        }

        public async Task Actualizar(TblJuradoXUnidadOrganizativa juradoXUnidad)
        {
            IEnumerable<int> result = await _database.GetData<int>("fn_tbljuradoxunidadorganizativa_actualizar", new
            {
                ljuradoxfacultad_id = juradoXUnidad.lJuradoXFacultad_id,
                ljurado_id = juradoXUnidad.lJurado_id,
                luniversidadorganizativa_id = juradoXUnidad.lUniversidadOrganizativa_id
            });
        }

        public async Task Eliminar(int idJuradoxFacultad)
        {
            IEnumerable<int> result = await _database.GetData<int>("fn_tbljuradoxunidadorganizativa_eliminar", new
            {
                ljuradoxfacultad_id = idJuradoxFacultad
            });
        }

        public async Task<TblJuradoXUnidadOrganizativa> ObtenerPorId(int idJuradoXFacultad)
        {
            IEnumerable<TblJuradoXUnidadOrganizativa> result = await _database.GetData<TblJuradoXUnidadOrganizativa>("fn_tbljuradoxunidadorganizativa_obtenerporid", new
            {
                ljuradoxfacultad_id = idJuradoXFacultad
            });
            return result.FirstOrDefault();
        }

        public async Task<List<TblJuradoXUnidadOrganizativa>> ObtenerTodos()
        {
            IEnumerable<TblJuradoXUnidadOrganizativa> result = await _database.GetData<TblJuradoXUnidadOrganizativa>("fn_tbljuradoxunidadorganizativa_obtenertodos");
            return result.ToList();
        }
    }
}