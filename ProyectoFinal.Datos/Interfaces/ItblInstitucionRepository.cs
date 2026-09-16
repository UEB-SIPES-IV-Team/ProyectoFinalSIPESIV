using ProyectoFinal.Datos.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Datos.Interfaces
{
    public interface ITblInstitucionRepository
    {
        public Task<int> Crear(TblInstitucion institucion);
        public Task<int> Actualizar(TblInstitucion institucion);
        public Task<int> Eliminar(int idInstitucion);
        public Task<TblInstitucion> ObtenerPorId(int idInstitucion);
        public Task<List<TblInstitucion>> ObtenerInstituciones();
    }
}
