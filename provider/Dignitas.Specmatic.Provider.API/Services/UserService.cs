namespace Dignitas.Specmatic.Provider.API.Services;

public class UserService
{
    private static readonly HashSet<uint> Users = [.. Enumerable.Range(0, 10).Select(i => (uint)i)];

    public bool Exists(uint userId)
    {
        return Users.Contains(userId);
    }
}
