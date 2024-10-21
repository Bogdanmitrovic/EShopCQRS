using MediatR;

namespace CommandHandler.Features.Proizvodi.Commands.Create;

public record CreateProizvod(string Naziv, string Opis, decimal Cena) : IRequest<Guid>;