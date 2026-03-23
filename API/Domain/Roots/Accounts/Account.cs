using Domain.Common;

namespace Domain.Roots.Accounts;
public class Account : BasicEntity
{
    public string ExternalId { get; protected set; } = null!;
    public string Name { get; protected set; } = null!;
    public string Username { get; protected set; } = null!;

    private Account() { }

    public Account(string externalId, string name, string username)
    {
        ExternalId = externalId;
        Name = name;
        Username = username;
    }
}
