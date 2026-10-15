using ProyectoFinal.Datos.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Datos.Interfaces
{
    public interface ITblSubParametroRepository
    {
        public Task<int> Crear(TblSubParametro subParametro);
        public Task<int> Actualizar(TblSubParametro subParametro);
        public Task<int> Eliminar(int idSubParametro);
        public Task<TblSubParametro> ObtenerPorId(int idSubParametro);
        public Task<List<TblSubParametro>> ObtenerSubParametros();
        public Task<List<TblSubParametro>> ObtenerSubparametrosTipoArea();
        public Task<List<TblSubParametro>> ObtenerSubParametrosTipoUnidadAsync();
        public Task<List<TblSubParametro>> ObtenerSubParametrosTipoInstitucionAsync();

    }
}
