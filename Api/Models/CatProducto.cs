namespace Api.Models
{
    public class CatProducto
    {
        // Primary Key
        public int Id { get; set; }
        // Properties
        public required string NombreProducto { get; set; }
        public required string ImagenProducto { get; set; }
        public required decimal Precio { get; set; }
        public required string Ext { get; set; }
        // Timestamps
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}

