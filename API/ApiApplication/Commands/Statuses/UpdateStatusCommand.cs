using Domain.Roots.Statuses.Services;
using FluentValidation;
using MediatR;

namespace ApiApplication.Commands.Statuses;

public class UpdateStatusCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Color { get; set; } = null!;
}

public class UpdateStatusCommandValidator : AbstractValidator<UpdateStatusCommand>
{
    public UpdateStatusCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Imię jest wymagane");
        RuleFor(x => x.Color).NotEmpty().WithMessage("Kolor jest wymagany");
    }
}

public class UpdateStatusCommandHandler : IRequestHandler<UpdateStatusCommand>
{
    private readonly IStatusService _statusService;

    public UpdateStatusCommandHandler(IStatusService statusService)
    {
        _statusService = statusService;
    }

    public async Task Handle(UpdateStatusCommand request, CancellationToken cancellationToken)
    {
        await _statusService.UpdateStatus(request.Id, request.Name, request.Color);
    }
}