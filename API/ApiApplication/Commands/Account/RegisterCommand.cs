using Domain.Enums;
using Domain.Roots.Accounts.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Supabase;

namespace ApiApplication.Bookings.Commands;

public class RegisterCommand : IRequest
{
    public string Name { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Imię jest wymagane");
        RuleFor(x => x.Username).NotEmpty().WithMessage("Nazwa użytkownika jest wymagana");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Hasło jest wymagane");
    }
}

public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
{
    private readonly IAccountService _accountService;
    private readonly IConfiguration _configuration;

    public RegisterCommandHandler(IAccountService accountService, IConfiguration configuration)
    {
        _accountService = accountService;
        _configuration = configuration;
    }

    public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var account = await _accountService.GetAccountByUsername(request.Username);
        if (account != null)
        {
            throw new ValidationException($"Użytkownik {request.Username} już istnieje");
        }

        var options = new SupabaseOptions
        {
            AutoConnectRealtime = true
        };

        var supabase = new Client(_configuration[ConfigConsts.SupabaseUrl], _configuration[ConfigConsts.SupabaseKey], options);

        await supabase.InitializeAsync();

        var session = await supabase.Auth.SignUp(request.Username, request.Password);

        await _accountService.AddAccount(session.User.Id, request.Name, request.Username, request.Password);
    }
}