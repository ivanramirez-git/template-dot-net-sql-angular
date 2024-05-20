using Api.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Api.Repository.Implement.TblFacturaRepository
{
    public class TblFacturaRepositoryImpl : ITblFacturaRepository
    {
        private readonly string _strConn;
        public TblFacturaRepositoryImpl(IConfiguration configuration)
        {
            _strConn = configuration?.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<TblFactura?> Create(TblFactura entity)
        {
            using (var conn = new SqlConnection(_strConn))
            {
                using (var cmd = new SqlCommand("spCreateTblFactura", conn))
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

        public async Task<IEnumerable<TblFactura>> GetAll()
        {
            using (var conn = new SqlConnection(_strConn))
            {
                using (var cmd = new SqlCommand("spGetAllTblFactura", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        var list = new List<TblFactura>();
                        while (await reader.ReadAsync())
                        {
                            list.Add(MapToValue(reader));
                        }
                        return list;
                    }
                }
            }
        }

        public async Task<TblFactura?> GetById(int id)
        {
            using (var conn = new SqlConnection(_strConn))
            {
                using (var cmd = new SqlCommand("spGetByIdTblFactura", conn))
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

        private SqlParameter[] MapToParameters(TblFactura entity)
        {
            return new SqlParameter[]{
                new ("@Id", entity.Id),
                new ("@IdCliente", entity.IdCliente),
                new ("@NumeroDeFactura", entity.NumeroDeFactura),
            };
        }

        public async Task<IEnumerable<TblFactura>> GetFacturasByCliente(int idCliente)
        {
            using (var conn = new SqlConnection(_strConn))
            {
                using (var cmd = new SqlCommand("spGetFacturasByCliente", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCliente", idCliente);
                    conn.Open();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        var list = new List<TblFactura>();
                        while (await reader.ReadAsync())
                        {
                            list.Add(MapToValue(reader));
                        }
                        return list;
                    }
                }
            }
        }

        public async Task<IEnumerable<TblFactura>> GetFacturasByNumeroFactura(int numeroFactura)
        {
            using (var conn = new SqlConnection(_strConn))
            {
                using (var cmd = new SqlCommand("spGetFacturasByNumeroFactura", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NumeroFactura", numeroFactura);
                    conn.Open();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        var list = new List<TblFactura>();
                        while (await reader.ReadAsync())
                        {
                            list.Add(MapToValue(reader));
                        }
                        return list;
                    }
                }
            }
        }

        private TblFactura MapToValue(SqlDataReader reader)
        {
            return new TblFactura()
            {
                Id = (int)reader["Id"],
                FechaEmisionFactura = (DateTime)reader["FechaEmisionFactura"],
                IdCliente = (int)reader["IdCliente"],
                NumeroDeFactura = (int)reader["NumeroDeFactura"],
                NumeroDeProductos = (int)reader["NumeroDeProductos"],
                SubTotalFactura = (decimal)reader["SubTotalFactura"],
                TotalImpuestos = (decimal)reader["TotalImpuestos"],
                TotalFactura = (decimal)reader["TotalFactura"],
                CreatedAt = (DateTime)reader["CreatedAt"],
                UpdatedAt = (DateTime)reader["UpdatedAt"],
                DeletedAt = (reader["DeletedAt"] == DBNull.Value) ? null : (DateTime?)reader["DeletedAt"],
                Cliente = new TblCliente()
                {
                    Id = (int)reader["Cliente_Id"],
                    RazonSocial = (string)reader["Cliente_RazonSocial"],
                    IdTipoCliente = (int)reader["Cliente_IdTipoCliente"],
                    FechaDeCreacion = (DateTime)reader["Cliente_FechaDeCreacion"],
                    RFC = (string)reader["Cliente_RFC"],
                    CreatedAt = (DateTime)reader["Cliente_CreatedAt"],
                    UpdatedAt = (DateTime)reader["Cliente_UpdatedAt"],
                    DeletedAt = (reader["Cliente_DeletedAt"] == DBNull.Value) ? null : (DateTime?)reader["Cliente_DeletedAt"],
                    TipoCliente = new CatTipoCliente()
                    {
                        Id = (int)reader["TipoCliente_Id"],
                        TipoCliente = (string)reader["TipoCliente_TipoCliente"],
                        CreatedAt = (DateTime)reader["TipoCliente_CreatedAt"],
                        UpdatedAt = (DateTime)reader["TipoCliente_UpdatedAt"],
                        DeletedAt = (reader["TipoCliente_DeletedAt"] == DBNull.Value) ? null : (DateTime?)reader["TipoCliente_DeletedAt"]
                    }
                }
            };
        }
    }
}