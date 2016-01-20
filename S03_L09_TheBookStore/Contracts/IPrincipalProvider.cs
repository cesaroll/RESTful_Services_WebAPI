using System.Security.Principal;

namespace TheBookStore.Contracts
{
    public interface IPrincipalProvider
    {
        IPrincipal CreatePrincipal(string username, string password);
    }
}