using MediatR;
using QueryHandler.Features.Proizvodi.DTOs;

namespace QueryHandler.Features.Proizvodi.Queries.Get;

public record GetProizvod(Guid Id) : IRequest<ProizvodDTO?>;