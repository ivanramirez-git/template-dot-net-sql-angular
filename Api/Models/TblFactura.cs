namespace Api.Models
{
    public class TblFactura
    {
        // Primary Key
        public int Id { get; set; }
        // Properties
        public DateTime FechaEmisionFactura { get; set; }
        public required int IdCliente { get; set; }
        public int NumeroDeFactura { get; set; }
        public int NumeroDeProductos { get; set; }
        public decimal SubTotalFactura { get; set; }
        public decimal TotalImpuestos { get; set; }
        public decimal TotalFactura { get; set; }
        // Timestamps
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        // Foreign Keys
        public TblCliente? Cliente { get; set; }
        // Adicional Properties
        public List<TblDetalleFactura>? DetalleFactura { get; set; }
    }

}
