namespace APIGateway.Models;

public record Proizvod
{
    public Guid? Id { get; set; }
    public string? Naziv { get; set; } = default!;
    public string? Opis { get; set; } = default!;
    public decimal? Cena { get; set; }
}