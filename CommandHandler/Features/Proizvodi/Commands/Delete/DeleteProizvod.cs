using MediatR;

namespace CommandHandler.Features.Proizvodi.Commands.Delete;

public record DeleteProizvod(Guid Id) : IRequest;