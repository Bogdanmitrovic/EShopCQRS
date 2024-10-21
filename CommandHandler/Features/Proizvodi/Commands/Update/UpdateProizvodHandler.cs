using CommandHandler.Persistence;
using MediatR;

namespace CommandHandler.Features.Proizvodi.Commands.Update;

public class UpdateProizvodHandler(ProizvodDbContext context) : IRequestHandler<UpdateProizvod>
{
    public async Task Handle(UpdateProizvod request, CancellationToken cancellationToken)
    {
        var product =
            await context.Proizvodi.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);
        if (product == null) return;
        product.Naziv = request.Naziv;
        product.Opis = request.Opis;
        product.Cena = request.Cena;
        await context.SaveChangesAsync(cancellationToken);
    }
}