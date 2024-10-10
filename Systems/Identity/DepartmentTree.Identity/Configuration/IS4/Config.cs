using Duende.IdentityServer.Models;

namespace DepartmentTree.Identity.Configuration;

public static class Config
{
    public static IEnumerable<Client> GetClients()
    {
        return new List<Client>
        {
            new Client
            {
                ClientId = "service_b",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedScopes = { "service_a" }
            }
        };
    }

    public static IEnumerable<ApiScope> GetApiScopes()
    {
        return new List<ApiScope>
        {
            new ApiScope("service_a", "Service A API")
        };
    }

    public static IEnumerable<ApiResource> GetApiResources()
    {
        return new List<ApiResource>
        {
            new ApiResource("service_a", "Service A API")
            {
                Scopes = { "service_a" }
            }
        };
    }

    public static IEnumerable<IdentityResource> GetIdentityResources()
    {
        return new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };
    }
}
