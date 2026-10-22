using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Negocio.DTOs.TblVisitante;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Negocio.Interfaces
{
    public interface ITblVisitanteService
    {
        //CRUD
        public Task Crear(CreateTblVisitanteDTO visitante);
        public Task Actualizar(UpdateTblVisitanteDTO visitante);
        public Task Eliminar(int idVisitante);
        public Task<List<ReadTblVisitanteDTO>> ObtenerTodos();
        public Task<ReadTblVisitanteDTO> ObtenerPorId(int idVisitante);
        public Task<ReadTblVisitanteDTO?> ObtenerPorCiYEvento(string ci, int idEvento);

    }
}
