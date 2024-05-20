namespace Api.Models
{
    public class TblCliente
    {
        // Primary Key
        public int Id { get; set; }
        // Properties
        public required string? RazonSocial { get; set; }
        public required int IdTipoCliente { get; set; }
        public required string? RFC { get; set; }
        public DateTime FechaDeCreacion { get; set; }
        // Timestamps
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        // Foreign Keys
        public CatTipoCliente? TipoCliente { get; set; }
    }

}