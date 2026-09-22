using ProyectoFinal.Datos.DataAccess;
using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Datos.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Datos.Repository
{
    public class TblVisitanteRepository : ITblVisitanteRepository
    {
        private readonly ProyectoFinalDatabase _database;
        public TblVisitanteRepository(ProyectoFinalDatabase database)
        {
            _database = database;
        }
        public async Task Actualizar(TblVisitante visitante)
        {
            IEnumerable<int> personaResult = await _database.GetData<int>("fn_tblvisitante_actualizar", new TblVisitante
            {
                lVisitante_id = visitante.lVisitante_id,
                lEvento_id = visitante.lEvento_id,
                sVisitante_nm = visitante.sVisitante_nm,
                sVisitante_ci = visitante.sVisitante_ci,
                sVisitante_email = visitante.sVisitante_email,
                sVisitante_telf = visitante.sVisitante_telf,
                sVisitante_inst = visitante.sVisitante_inst
            });
        }

        public async Task Crear(TblVisitante visitante)
        {
            IEnumerable<int> personaResult = await _database.GetData<int>("fn_tblvisitante_crear", new TblVisitante
            {
                sVisitante_nm = visitante.sVisitante_nm,
                sVisitante_ci = visitante.sVisitante_ci,
                sVisitante_email = visitante.sVisitante_email,
                sVisitante_telf = visitante.sVisitante_telf,
                sVisitante_inst = visitante.sVisitante_inst
            });
        }

        public async Task Eliminar(int idVisitante)
        {
            IEnumerable<int> personaResult = await _database.GetData<int>("fn_tblvisitante_eliminar", new TblVisitante
            {
                lVisitante_id = idVisitante
            });
        }

        public async Task<TblVisitante> ObtenerPorId(int idVisitante)
        {
            IEnumerable<TblVisitante> personaResult = await _database.GetData<TblVisitante>("fn_tblvisitante_obtenerporid", new TblVisitante
            {
                lVisitante_id = idVisitante
            });
            return personaResult.FirstOrDefault();
        }

        public async Task<List<TblVisitante>> ObtenerTodos()
        {
            IEnumerable<TblVisitante> personaResult = await _database.GetData<TblVisitante>("fn_tblvisitante_obtenertodos");
            return personaResult.ToList();
        }
    }
}
