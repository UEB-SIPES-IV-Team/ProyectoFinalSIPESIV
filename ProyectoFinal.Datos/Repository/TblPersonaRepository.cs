using ProyectoFinal.Datos.DataAccess;
using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Datos.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Datos.Repository
{
   public class TblPersonaRepository : ITblPersonaRepository
    {
        private readonly ProyectoFinalDatabase _database;
        public TblPersonaRepository(ProyectoFinalDatabase database)
        {
            _database = database;
        }

        public async Task Actualizar(TblPersona entidad)
        {
            await _database.GetData<int?>(
                "fn_tblpersona_actualizar",
                new
                {
                    p_lpersona_id = entidad.lPersona_id,
                    p_spersona_nm = entidad.sPersona_nm,
                    p_spersona_aps = entidad.sPersona_aps,
                    p_spersona_email = entidad.sPersona_email,
                    p_spersona_telf = entidad.sPersona_telf,
                    p_spersona_tipo_persona =
                        entidad.sPersona_tipo_persona,
                    p_spersona_sexo = entidad.sPersona_sexo,
                    p_spersona_ci = entidad.sPersona_ci,
                    p_spersona_fecha_nacimiento =
                        entidad.dPersona_fecha_nacimiento
                });
        }




        public async Task Crear(TblPersona persona)
        {
            await _database.GetData<int>(
                "fn_tblpersona_crear",
                new
                {
                    p_spersona_nm = persona.sPersona_nm,
                    p_spersona_aps = persona.sPersona_aps,
                    p_spersona_email = persona.sPersona_email,
                    p_spersona_telf = persona.sPersona_telf,
                    p_spersona_tipo_persona = persona.sPersona_tipo_persona,
                    p_spersona_sexo = persona.sPersona_sexo,
                    p_spersona_ci = persona.sPersona_ci,
                    p_dpersona_fecha_nacimiento =
                        persona.dPersona_fecha_nacimiento
                });
        }


        public async Task Eliminar(int idPersona)
        {
            await _database.GetData<dynamic>("fn_tblpersona_eliminar", new
            {
                p_lpersona_id = idPersona
            });
        }

        public async Task<TblPersona> ObtenerPorId(int idPersona)
        {
            IEnumerable<TblPersona> result = await _database.GetData<TblPersona>(
                "fn_tblpersona_obtenerporid",
                new { p_lpersona_id = idPersona } 
            );
            return result.FirstOrDefault();
        }

        public async Task<List<TblPersona>> ObtenerTodos()
        {
            IEnumerable<TblPersona> personaResult = await _database.GetData<TblPersona>("fn_tblpersona_obtenertodos");
            return personaResult.ToList();
        }
    }
}
