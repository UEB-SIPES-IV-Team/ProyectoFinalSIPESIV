using ProyectoFinal.Datos.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Datos.Interfaces
{
    public interface ITblPremiacionRepository
    {
        //CRUD
        public Task<int> Crear(TblPremiacion premiacion);
        public Task<TblPremiacion> ObtenerPorId(int idPremiacion);
        public Task<List<TblPremiacion>> ObtenerTodos();
        public Task Actualizar (TblPremiacion premiacion);
        public Task Eliminar(int idPremiacion);



    }
}
