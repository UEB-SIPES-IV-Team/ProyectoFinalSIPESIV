using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Datos.Interfaces;
using ProyectoFinal.Negocio.DTOs.TblEvaluacionJurado;
using ProyectoFinal.Negocio.Interfaces; 
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Negocio.Interfaces
{
    public interface ITblEvaluacionJuradoService
    {
        public Task Crear(CreateTblEvaluacionJuradoDTO evaluacionJuradoe);
        public Task Actualizar(UpdateTblEvaluacionJuradoDTO evaluacionJurado);
        public Task Eliminar(int idEvaluacionVisitante);
        public Task<List<ReadTblEvaluacionJuradoDTO>> ObtenerTodos();
        public Task<ReadTblEvaluacionJuradoDTO> ObtenerPorId(int idEvaluacionJurado);
    }
}
