namespace Api.Models
{
    public class CatTipoCliente
    {
        public int Id { get; set; }
        public required string? TipoCliente { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}