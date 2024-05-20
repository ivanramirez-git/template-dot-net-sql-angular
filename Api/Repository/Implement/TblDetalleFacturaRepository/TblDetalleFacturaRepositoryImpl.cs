using Api.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Api.Repository.Implement.TblDetalleFacturaRepository
{
    public class TblDetalleFacturaRepositoryImpl : ITblDetalleFacturaRepository
    {
        private readonly string _strConn;
        public TblDetalleFacturaRepositoryImpl(IConfiguration configuration)
        {
            _strConn = configuration?.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<TblDetalleFactura?> Create(TblDetalleFactura entity)
        {
            using (var conn = new SqlConnection(_strConn))
            {
                using (var cmd = new SqlCommand("spCreateTblDetalleFactura", conn))
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

        public async Task<IEnumerable<TblDetalleFactura>> GetAll()
        {
            using (var conn = new SqlConnection(_strConn))
            {
                using (var cmd = new SqlCommand("spGetAllTblDetalleFactura", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        var list = new List<TblDetalleFactura>();
                        while (await reader.ReadAsync())
                        {
                            list.Add(MapToValue(reader));
                        }
                        return list;
                    }
                }
            }
        }

        public async Task<TblDetalleFactura?> GetById(int id)
        {
            using (var conn = new SqlConnection(_strConn))
            {
                using (var cmd = new SqlCommand("spGetByIdTblDetalleFactura", conn))
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

        private SqlParameter[] MapToParameters(TblDetalleFactura entity)
        {
            return new SqlParameter[]
            {
                new ("@Id", entity.Id), // Para actualizar y eliminar
                new ("@IdFactura", entity.IdFactura),
                new ("@IdProducto", entity.IdProducto),
                new ("@CantidadDeProducto", entity.CantidadDeProducto),
                new ("@Notas", entity.Notas)
            };
        }
        private TblDetalleFactura MapToValue(SqlDataReader reader)
        {
            return new TblDetalleFactura()
            {
                Id = (int)reader["Id"],
                IdFactura = (int)reader["IdFactura"],
                IdProducto = (int)reader["IdProducto"],
                CantidadDeProducto = (int)reader["CantidadDeProducto"],
                PrecioUnitario = (decimal)reader["PrecioUnitario"],
                SubTotal = (decimal)reader["SubTotal"],
                Notas = reader["Notas"] as string,
                CreatedAt = (DateTime)reader["CreatedAt"],
                UpdatedAt = (DateTime)reader["UpdatedAt"],
                DeletedAt = (reader["DeletedAt"] == DBNull.Value) ? null : (DateTime?)reader["DeletedAt"],
                Factura = new TblFactura()
                {
                    Id = (int)reader["IdFactura"],
                    FechaEmisionFactura = (DateTime)reader["FechaEmisionFactura"],
                    IdCliente = (int)reader["IdCliente"],
                    NumeroDeFactura = (int)reader["NumeroDeFactura"],
                    NumeroDeProductos = (int)reader["NumeroDeProductos"],
                    SubTotalFactura = (decimal)reader["FacturaSubTotalFactura"],
                    TotalImpuestos = (decimal)reader["FacturaTotalImpuestos"],
                    TotalFactura = (decimal)reader["FacturaTotalFactura"],
                    Cliente = new TblCliente()
                    {
                        Id = (int)reader["Cliente_Id"],
                        RazonSocial = reader["Cliente_RazonSocial"] as string,
                        IdTipoCliente = (int)reader["Cliente_IdTipoCliente"],
                        RFC = reader["Cliente_RFC"] as string,
                        FechaDeCreacion = (DateTime)reader["Cliente_FechaDeCreacion"],
                        TipoCliente = new CatTipoCliente()
                        {
                            Id = (int)reader["TipoCliente_Id"],
                            TipoCliente = reader["TipoCliente_TipoCliente"] as string,
                            CreatedAt = (DateTime)reader["TipoCliente_CreatedAt"],
                            UpdatedAt = (DateTime)reader["TipoCliente_UpdatedAt"],
                            DeletedAt = (reader["TipoCliente_DeletedAt"] == DBNull.Value) ? null : (DateTime?)reader["TipoCliente_DeletedAt"]
                        }
                    }
                }
            };
        }

    }
}




