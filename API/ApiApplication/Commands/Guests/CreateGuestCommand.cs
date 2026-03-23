using Domain.Roots.Guests;
using Domain.Roots.Guests.Services;
using FluentValidation;
using MediatR;

namespace ApiApplication.Bookings.Guests;

public class CreateGuestCommand : IRequest
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
}

public class CreateGuestCommandValidator : AbstractValidator<CreateGuestCommand>
{
    public CreateGuestCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Imię jest wymagane");
        RuleFor(x => x.Surname).NotEmpty().WithMessage("Nazwisko jest wymagane");
    }
}

public class CreateGuestCommandHandler : IRequestHandler<CreateGuestCommand>
{
    private readonly IGuestService _guestService;

    public CreateGuestCommandHandler(IGuestService guestService)
    {
        _guestService = guestService;
    }

    public async Task Handle(CreateGuestCommand request, CancellationToken cancellationToken)
    {
        var newGuest = new Guest(request.Name, request.Surname);

        await _guestService.AddGuest(newGuest);
    }
}