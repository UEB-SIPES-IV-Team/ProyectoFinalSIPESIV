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
            IEnumerable<int> personaResult = await _database.GetData<int>("fn_tblvisitante_actualizar", new
            {
                p_lvisitante_id = visitante.lVisitante_id,
                p_levento_id = visitante.lEvento_id,
                p_svisitante_nm = visitante.sVisitante_nm,
                p_svisitante_email = visitante.sVisitante_email,
                p_svisitante_telf = visitante.sVisitante_telf,
                p_svisitante_ci = visitante.sVisitante_ci,
                p_svisitante_inst = visitante.sVisitante_inst,
                p_svisitante_fecha_nacimiento = visitante.sVisitante_fecha_nacimiento,
                p_svisitante_sexo = visitante.sVisitante_sexo,
                p_svisitante_carrera = visitante.sVisitante_carrera
            });
        }

        public async Task Crear(TblVisitante visitante)
        {
            IEnumerable<int> personaResult = await _database.GetData<int>("fn_tblvisitante_crear", new
            {
                lEvento_id = visitante.lEvento_id,
                sVisitante_nm = visitante.sVisitante_nm,
                sVisitante_ci = visitante.sVisitante_ci,
                sVisitante_email = visitante.sVisitante_email,
                sVisitante_telf = visitante.sVisitante_telf,
                sVisitante_inst = visitante.sVisitante_inst,    
                sVisitante_fecha_nacimiento = visitante.sVisitante_fecha_nacimiento,
                sVisitante_sexo = visitante.sVisitante_sexo,
                sVisitante_carrera = visitante.sVisitante_carrera,

            });
        }

        public async Task Eliminar(int idVisitante)
        {
            await _database.GetData<dynamic>("fn_tblvisitante_eliminar", new
            {
                lvisitante_id = idVisitante
            });
        }
        public async Task<TblVisitante> ObtenerPorId(int idVisitante)
        {
            IEnumerable<TblVisitante> personaResult = await _database.GetData<TblVisitante>("fn_tblvisitante_obtenerporid", new
            {
                lvisitante_id = idVisitante
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
