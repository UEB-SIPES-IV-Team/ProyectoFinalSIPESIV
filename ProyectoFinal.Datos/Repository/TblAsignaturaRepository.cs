using ProyectoFinal.Datos.DataAccess;
using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Datos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.Datos.Repository
{
    public class TblAsignaturaRepository : ITblAsignaturaRepository
    {
        private readonly ProyectoFinalDatabase _database;

        public TblAsignaturaRepository(ProyectoFinalDatabase database)
        {
            _database = database;
        }

        public async Task<int> Crear(TblAsignatura asignatura)
        {
            IEnumerable<int> resultado = await _database.GetData<int>("fn_tblasignatura_crear", new
            {
                lArea_id = asignatura.lArea_id,
                lAsignatura_slog = asignatura.lAsignatura_slog,
                lAsignatura_nm = asignatura.lAsignatura_nm,
                bEstado = asignatura.bEstado
            });
            return resultado.FirstOrDefault();
        }

        public async Task<int> Actualizar(TblAsignatura asignatura)
        {
            // Corregido: Objeto anónimo con los 5 parámetros necesarios (incluyendo bEstado)
            IEnumerable<int> resultado = await _database.GetData<int>("fn_tblasignatura_actualizar", new
            {
                lAsignatura_id = asignatura.lAsignatura_id,
                lArea_id = asignatura.lArea_id,
                lAsignatura_slog = asignatura.lAsignatura_slog,
                lAsignatura_nm = asignatura.lAsignatura_nm,
                bEstado = asignatura.bEstado
            });
            return resultado.FirstOrDefault();
        }

        public async Task<int> Eliminar(int asignatura)
        {
            IEnumerable<int> resultado = await _database.GetData<int>("fn_tblasignatura_eliminar", new
            {
                lAsignatura_id = asignatura
            });
            return resultado.FirstOrDefault();
        }

        public async Task<List<TblAsignatura>> ObtenerAsignaturas()
        {
            IEnumerable<TblAsignatura> resultado = await _database.GetData<TblAsignatura>("fn_tblasignatura_obtener_asignaturas");
            return resultado.ToList();
        }

        public async Task<TblAsignatura> ObtenerPorId(int asignatura)
        {
            IEnumerable<TblAsignatura> resultado = await _database.GetData<TblAsignatura>("fn_tblasignatura_obtener_por_id", new
            {
                lAsignatura_id = asignatura
            });

            return resultado.FirstOrDefault();
        }
    }
}