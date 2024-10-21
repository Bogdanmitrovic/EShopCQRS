using MediatR;
using Microsoft.EntityFrameworkCore;
using QueryHandler.Features.Proizvodi.DTOs;
using QueryHandler.Persistence;

namespace QueryHandler.Features.Proizvodi.Queries.List;

public class ListProizvodHandler(ProizvodDbContext context) : IRequestHandler<ListProizvod, List<ProizvodDTO>>
{
    public async Task<List<ProizvodDTO>> Handle(ListProizvod request, CancellationToken cancellationToken)
    {
        return await context.Proizvodi
            .Select(p => new ProizvodDTO(p.Id, p.Naziv, p.Opis, p.Cena))
            .ToListAsync(cancellationToken: cancellationToken);
    }
}