namespace Api.Models
{
    public class TblDetalleFactura
    {
        // Primary Key
        public int Id { get; set; }
        // Properties
        public required int IdFactura { get; set; }
        public required int IdProducto { get; set; }
        public required int CantidadDeProducto { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal SubTotal { get; set; }
        public required string? Notas { get; set; }
        // Timestamps
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        // Foreign Keys
        public TblFactura? Factura { get; set; }
        public CatProducto? Producto { get; set; }
    }
}