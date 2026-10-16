using ProyectoFinal.Datos.DataAccess;
using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Datos.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Datos.Repository
{
    public class TblUsuarioRepository : ITblUsuarioRepository
    {
        private readonly ProyectoFinalDatabase _database;
        public TblUsuarioRepository(ProyectoFinalDatabase database)
        {
            _database = database;
        }

        public async Task Actualizar(TblUsuario usuario)
        {
            IEnumerable<int> usuarioResult = await _database.GetData<int>("fn_tblusuario_actualizar", new
            {
                p_lUsuario_id = usuario.lUsuario_id,
                p_lPersona_id = usuario.lPersona_id,
                p_lRol_id = usuario.lRol_id,
                p_sUsername = usuario.sUsername,
                p_sPassword = usuario.sPassword,
                p_bEstado = usuario.bEstado
            });
        }

        public async Task Crear(TblUsuario usuario)
        {
            IEnumerable<int> usuarioResult = await _database.GetData<int>("fn_tblusuario_crear", new
            {
                p_lPersona_id = usuario.lPersona_id,
                p_lRol_id = usuario.lRol_id,
                p_sUsername = usuario.sUsername,
                p_sPassword = usuario.sPassword,
                p_bEstado = usuario.bEstado
            });
        }

        public async Task Eliminar(int idUsuario)
        {
            IEnumerable<int> usuarioResult = await _database.GetData<int>("fn_tblusuario_eliminar", new
            {
                p_lUsuario_id = idUsuario
            });
        }

        public async Task<TblUsuario> ObtenerPorId(int idUsuario)
        {
            IEnumerable<TblUsuario> usuarioResult = await _database.GetData<TblUsuario>("fn_tblusuario_obtenerporid", new
            {
                p_lUsuario_id = idUsuario
            });
            return usuarioResult.FirstOrDefault();
        }

        public async Task<List<TblUsuario>> ObtenerTodos()
        {
            IEnumerable<TblUsuario> usuarioResult = await _database.GetData<TblUsuario>("fn_tblusuario_obtenertodos");
            return usuarioResult.ToList();
        }

       
        public async Task<TblUsuario?> ObtenerPorUsername(string username)
        {
            IEnumerable<TblUsuario> resultado =
                await _database.GetData<TblUsuario>(
                    "fn_tblusuario_obtenerporusername",
                    new
                    {
                        p_sUsername = username
                    });

            return resultado.FirstOrDefault();
        }
    }
}
