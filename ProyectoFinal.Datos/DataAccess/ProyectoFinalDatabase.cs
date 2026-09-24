using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.Datos.DataAccess
{
    public class ProyectoFinalDatabase
    {
        private readonly IConfiguration _configuration;
        private readonly string connection = "DefaultConnection";

        public ProyectoFinalDatabase(IConfiguration configuration)
        {
            _configuration = configuration;
            // 1. Permite mapear columnas en minúsculas/underscores a propiedades CamelCase en C#
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<IEnumerable<T>> GetData<T>(string functionName, object parameters = null)
        {
            using IDbConnection conn = new NpgsqlConnection(_configuration.GetConnectionString(connection));
            try
            {
                string sql;

                if (parameters == null)
                {
                    // 2. Agrega "* FROM" para que expanda las columnas del conjunto de resultados
                    sql = $"SELECT * FROM {functionName}()";
                }
                else
                {
                    // Obtener propiedades del objeto real
                    var properties = parameters.GetType().GetProperties();
                    var paramNames = string.Join(", ", properties.Select(p => $"@{p.Name}"));
                    // 2. Agrega "* FROM" aquí también si la función retorna una tabla con parámetros
                    sql = $"SELECT * FROM {functionName}({paramNames})";
                }

                var result = await conn.QueryAsync<T>(sql, parameters, commandType: CommandType.Text);
                return result;
            }
            catch (NpgsqlException ex)
            {
                throw new Exception($"Error al ejecutar la función {functionName}: {ex.Message}", ex);
            }
        }
    }
}