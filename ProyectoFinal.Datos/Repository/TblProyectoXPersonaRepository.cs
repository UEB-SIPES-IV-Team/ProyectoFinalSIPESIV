using ProyectoFinal.Datos.DataAccess;
using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Datos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.Datos.Repository
{
    public class TblProyectoXPersonaRepository : ITblProyectoXPersonaRepository
    {
        private readonly ProyectoFinalDatabase _database;

        public TblProyectoXPersonaRepository(ProyectoFinalDatabase database)
        {
            _database = database;
        }

        public async Task<int> Actualizar(TblProyectoXPersona proyectoxpersona)
        {
            IEnumerable<int> result = await _database.GetData<int>("fn_tblproyectoxpersona_actualizar", new
            {
                lProyectoXPersona_id = proyectoxpersona.lProyectoXPersona_id,
                lProyecto_id = proyectoxpersona.lProyecto_id,
                lPersona_id = proyectoxpersona.lPersona_id
            });
            return result.FirstOrDefault();
        }

        public async Task<int> Crear(TblProyectoXPersona proyectoxpersona)
        {
            IEnumerable<int> result = await _database.GetData<int>("fn_tblproyectoxpersona_crear", new
            {
                lProyecto_id = proyectoxpersona.lProyecto_id,
                lPersona_id = proyectoxpersona.lPersona_id
            });
            return result.FirstOrDefault();
        }

        public async Task Eliminar(int idProyectoXPersona)
        {
            await _database.GetData<dynamic>("fn_tblproyectoxpersona_eliminar", new
            {
                p_lproyectoxpersona_id = idProyectoXPersona
            });
        }
        public async Task<TblProyectoXPersona> ObtenerPorId(int idProyectoXPersona)
        {
            IEnumerable<TblProyectoXPersona> result = await _database.GetData<TblProyectoXPersona>(
                "fn_tblproyectoxpersona_obtenerporid",
                new { p_lproyectoxpersona_id = idProyectoXPersona }
            );
            return result.FirstOrDefault();
        }

        public async Task<List<TblProyectoXPersona>> ObtenerTodos()
        {
            IEnumerable<TblProyectoXPersona> result = await _database.GetData<TblProyectoXPersona>("fn_tblproyectoxpersona_obtenertodos");
            return result.ToList();
        }

        Task ITblProyectoXPersonaRepository.Actualizar(TblProyectoXPersona proyectoXPersona)
        {
            return Actualizar(proyectoXPersona);
        }

        Task ITblProyectoXPersonaRepository.Crear(TblProyectoXPersona proyectoXPersona)
        {
            return Crear(proyectoXPersona);
        }

        Task ITblProyectoXPersonaRepository.Eliminar(int idProyectoXPersona)
        {
            return Eliminar(idProyectoXPersona);
        }
    }
}