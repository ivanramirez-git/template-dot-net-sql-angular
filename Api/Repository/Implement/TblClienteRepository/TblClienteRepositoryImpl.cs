using Api.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Api.Repository.Implement.TblClienteRepository
{
    public class TblClienteRepositoryImpl : ITblClienteRepository
    {
        private readonly string _strConn;
        public TblClienteRepositoryImpl(IConfiguration configuration)
        {
            _strConn = configuration?.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<TblCliente?> Create(TblCliente entity)
        {
            using (var conn = new SqlConnection(_strConn))
            {
                using (var cmd = new SqlCommand("spCreateTblCliente", conn))
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

        public async Task<TblCliente?> Delete(int id)
        {
            using (var conn = new SqlConnection(_strConn))
            {
                using (var cmd = new SqlCommand("spDeleteTblCliente", conn))
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

        public async Task<IEnumerable<TblCliente>> GetAll()
        {
            var list = new List<TblCliente>();
            using (var conn = new SqlConnection(_strConn))
            {
                using (var cmd = new SqlCommand("spGetAllTblCliente", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            list.Add(MapToValue(reader));
                        }
                        return list;
                    }
                }
            }
        }

        public async Task<TblCliente?> GetById(int id)
        {
            using (var conn = new SqlConnection(_strConn))
            {
                using (var cmd = new SqlCommand("spGetByIdTblCliente", conn))
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

        public async Task<TblCliente?> Update(TblCliente entity)
        {
            using (var conn = new SqlConnection(_strConn))
            {
                using (var cmd = new SqlCommand("spUpdateTblCliente", conn))
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

        private SqlParameter[] MapToParameters(TblCliente entity)
        {
            return new SqlParameter[]
            {
                new ("@Id", entity.Id), // solo para update y delete
                new ("@RazonSocial", entity.RazonSocial),
                new ("@IdTipoCliente", entity.IdTipoCliente),
                new ("@RFC", entity.RFC),
            };
        }

        private TblCliente MapToValue(SqlDataReader reader)
        {
            return new TblCliente
            {
                Id = (int)reader["Id"],
                RazonSocial = (string)reader["RazonSocial"],
                IdTipoCliente = (int)reader["IdTipoCliente"],
                FechaDeCreacion = (DateTime)reader["FechaDeCreacion"],
                RFC = (string)reader["RFC"],
                CreatedAt = (DateTime)reader["CreatedAt"],
                UpdatedAt = (DateTime)reader["UpdatedAt"],
                DeletedAt = reader["DeletedAt"] is DBNull ? null : (DateTime?)reader["DeletedAt"],
                TipoCliente = new CatTipoCliente
                {
                    Id = (int)reader["TipoCliente_Id"],
                    TipoCliente = (string)reader["TipoCliente_TipoCliente"],
                    CreatedAt = (DateTime)reader["TipoCliente_CreatedAt"],
                    UpdatedAt = (DateTime)reader["TipoCliente_UpdatedAt"],
                    DeletedAt = reader["TipoCliente_DeletedAt"] is DBNull ? null : (DateTime?)reader["TipoCliente_DeletedAt"],
                }
            };
        }

    }
}