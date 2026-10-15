using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Negocio.DTOs.TblSubParametro;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Negocio.Interfaces
{
    public interface ITblSubParametroService
    {
        public Task Crear(CreateTblSubParametroDTO subParametro);
        public Task Actualizar(UpdateTblSubParametroDTO subParametro);
        public Task Eliminar(int idSubParametro);
        public Task<ReadTblSubParametroDTO> ObtenerPorId(int idSubParametro);
        public Task<List<ReadTblSubParametroDTO>> ObtenerSubParametros();
        public Task<List<ReadTblSubParametroDTO>> ObtenerSubparametrosTipoArea();
        public Task<List<ReadTblSubParametroDTO>> ObtenerSubParametrosTipoUnidadAsync();
        public Task<List<ReadTblSubParametroDTO>> ObtenerSubParametrosTipoInstitucionAsync();
    }
}
