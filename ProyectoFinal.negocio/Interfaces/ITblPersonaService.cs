using ProyectoFinal.Negocio.DTOs.TblPersona;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Negocio.Interfaces
{
    public interface ITblPersonaService
    {

        // CRUD
        public Task Crear(CreateTblPersonaDTO persona);
        public Task Actualizar(UpdateTblPersonaDTO persona);
        public Task Eliminar(int idPersona);
        public Task<List<ReadTblPersonaDTO>> ObtenerTodos();
        public Task<ReadTblPersonaDTO> ObtenerPorId(int idPersona);
    }
}
