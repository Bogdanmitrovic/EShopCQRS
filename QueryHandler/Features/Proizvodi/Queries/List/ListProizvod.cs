using MediatR;
using QueryHandler.Features.Proizvodi.DTOs;

namespace QueryHandler.Features.Proizvodi.Queries.List;

public record ListProizvod : IRequest<List<ProizvodDTO>>;