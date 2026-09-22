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
        public async Task Actualizar(TblPersona persona)
        {
            IEnumerable<int> personaResult = await _database.GetData<int>("fn_tblpersona_actualizar", new TblPersona
            {
                lPersona_id = persona.lPersona_id,
                sPersona_nm = persona.sPersona_nm,
                sPersona_aps = persona.sPersona_aps,
                sPersona_email = persona.sPersona_email,
                sPersona_telf = persona.sPersona_telf,
                sPersona_tipo_persona = persona.sPersona_tipo_persona
            });
        }

        public async Task Crear(TblPersona persona)
        {
            IEnumerable<int> personaResult = await _database.GetData<int>("fn_tblpersona_crear", new TblPersona
            {
                sPersona_nm = persona.sPersona_nm,
                sPersona_aps = persona.sPersona_aps,
                sPersona_email = persona.sPersona_email,
                sPersona_telf = persona.sPersona_telf,
                sPersona_tipo_persona = persona.sPersona_tipo_persona
            });
        }

        public async Task Eliminar(int idPersona)
        {
            IEnumerable<int> personaResult = await _database.GetData<int>("fn_tblpersona_eliminar", new TblPersona
            {
                lPersona_id = idPersona
            });
        }

        public async Task<TblPersona> ObtenerPorId(int idPersona)
        {
            IEnumerable<TblPersona> personaResult = await _database.GetData<TblPersona>("fn_tblpersona_obtenerporid", new TblPersona
            {
                lPersona_id = idPersona
            });
            return personaResult.FirstOrDefault();
        }

        public async Task<List<TblPersona>> ObtenerTodos()
        {
            IEnumerable<TblPersona> personaResult = await _database.GetData<TblPersona>("fn_tblpersona_obtenertodos");
            return personaResult.ToList();
        }
    }
}
