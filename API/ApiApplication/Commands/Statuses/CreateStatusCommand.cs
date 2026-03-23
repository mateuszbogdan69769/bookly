using Domain.Roots.Statuses;
using Domain.Roots.Statuses.Services;
using FluentValidation;
using MediatR;

namespace ApiApplication.Commands.Statuses;

public class CreateStatusCommand : IRequest
{
    public string Name { get; set; } = null!;
    public string Color { get; set; } = null!;
}

public class CreateStatusCommandValidator : AbstractValidator<CreateStatusCommand>
{
    public CreateStatusCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Imię jest wymagane");
        RuleFor(x => x.Color).NotEmpty().WithMessage("Kolor jest wymagany");
    }
}

public class CreateStatusCommandHandler : IRequestHandler<CreateStatusCommand>
{
    private readonly IStatusService _statusService;

    public CreateStatusCommandHandler(IStatusService statusService)
    {
        _statusService = statusService;
    }

    public async Task Handle(CreateStatusCommand request, CancellationToken cancellationToken)
    {
        var newStatus = new Status(request.Name, request.Color);

        await _statusService.AddStatus(newStatus);
    }
}