using Domain.Roots.Guests.Services;
using FluentValidation;
using MediatR;

namespace ApiApplication.Bookings.Guests;

public class UpdateGuestCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
}

public class UpdateGuestCommandValidator : AbstractValidator<UpdateGuestCommand>
{
    public UpdateGuestCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Imię jest wymagane");
        RuleFor(x => x.Surname).NotEmpty().WithMessage("Nazwisko jest wymagane");
    }
}

public class UpdateGuestCommandHandler : IRequestHandler<UpdateGuestCommand>
{
    private readonly IGuestService _guestService;

    public UpdateGuestCommandHandler(IGuestService guestService)
    {
        _guestService = guestService;
    }

    public async Task Handle(UpdateGuestCommand request, CancellationToken cancellationToken)
    {
        await _guestService.UpdateGuest(request.Id, request.Name, request.Surname);
    }
}