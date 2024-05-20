using Api.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Api.Repository.Implement.CatTipoClienteRepository
{
    public class CatTipoClienteRepositoryImpl : ICatTipoClienteRepository
    {
        private readonly string _strConn;
        public CatTipoClienteRepositoryImpl(IConfiguration configuration)
        {
            _strConn = configuration?.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<CatTipoCliente?> Create(CatTipoCliente entity)
        {
            using (var conn = new SqlConnection(_strConn))
            {
                using (var cmd = new SqlCommand("spCreateCatTipoCliente", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(MapToParameters(entity));
                    conn.Open();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return MapToValue(reader);
                        }
                        return null;
                    }
                }
            }
        }

        public async Task<CatTipoCliente?> Delete(int id)
        {
            using (var conn = new SqlConnection(_strConn))
            {
                using (var cmd = new SqlCommand("spDeleteCatTipoCliente", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return MapToValue(reader);
                        }
                        return null;
                    }
                }
            }
        }

        public async Task<IEnumerable<CatTipoCliente>> GetAll()
        {
            using (var conn = new SqlConnection(_strConn))
            {
                using (var cmd = new SqlCommand("spGetAllCatTipoCliente", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        var list = new List<CatTipoCliente>();
                        while (await reader.ReadAsync())
                        {
                            list.Add(MapToValue(reader));
                        }
                        return list;
                    }
                }
            }
        }

        public async Task<CatTipoCliente?> GetById(int id)
        {
            using (var conn = new SqlConnection(_strConn))
            {
                using (var cmd = new SqlCommand("spGetByIdCatTipoCliente", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return MapToValue(reader);
                        }
                        return null;
                    }
                }
            }
        }

        public async Task<CatTipoCliente?> Update(CatTipoCliente entity)
        {
            using (var conn = new SqlConnection(_strConn))
            {
                using (var cmd = new SqlCommand("spUpdateCatTipoCliente", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(MapToParameters(entity));
                    conn.Open();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return MapToValue(reader);
                        }
                        return null;
                    }
                }
            }
        }

        private SqlParameter[] MapToParameters(CatTipoCliente entity)
        {
            return new SqlParameter[]
            {
                new ("@Id", entity.Id), // Para actualizar y eliminar
                new ("@TipoCliente", entity.TipoCliente),
            };
        }

        private CatTipoCliente MapToValue(SqlDataReader reader)
        {
            return new CatTipoCliente()
            {
                Id = (int)reader["Id"],
                TipoCliente = (string)reader["TipoCliente"],
                CreatedAt = (DateTime)reader["CreatedAt"],
                UpdatedAt = (DateTime)reader["UpdatedAt"],
                DeletedAt = reader["DeletedAt"] as DateTime?,
            };
        }
    }

}
