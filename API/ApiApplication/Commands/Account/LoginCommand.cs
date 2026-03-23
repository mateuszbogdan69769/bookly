using Domain.Enums;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Supabase;

namespace ApiApplication.Bookings.Commands;

public class LoginCommand : IRequest<AuthorizationData?>
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("Nazwa użytkownika jest wymagana");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Hasło jest wymagane");
    }
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthorizationData?>
{
    private readonly IConfiguration _configuration;

    public LoginCommandHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<AuthorizationData?> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var options = new SupabaseOptions
        {
            AutoConnectRealtime = true
        };

        var supabase = new Client(_configuration[ConfigConsts.SupabaseUrl], _configuration[ConfigConsts.SupabaseKey], options);
        await supabase.InitializeAsync();

        var session = await supabase.Auth.SignIn(request.Username, request.Password);

        return new AuthorizationData()
        {
            AccessToken = session?.AccessToken,
            RefreshToken = session?.RefreshToken,
            ExpiresIn = (int)session?.ExpiresIn,
        };
    }
}

public class AuthorizationData
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public int ExpiresIn { get; set; }
}