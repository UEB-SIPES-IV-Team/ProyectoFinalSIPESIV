using ProyectoFinal.Datos.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Datos.Interfaces
{
    public interface ITblProyectoXPersonaRepository
    {
        //CRUD
        public Task Crear(TblProyectoXPersona proyectoXPersona);
        public Task<TblProyectoXPersona> ObtenerPorId(int idProyectoXPersona);
        public Task<List<TblProyectoXPersona>> ObtenerTodos();
        public Task Actualizar(TblProyectoXPersona proyectoXPersona);
        public Task Eliminar(int idProyectoXPersona);
        public Task<List<TblProyectoXPersona>> ObtenerIntegrantesPorProyectoAsync(long idProyecto);
    }
}
