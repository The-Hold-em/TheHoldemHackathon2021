using Duende.IdentityServer;
using Duende.IdentityServer.Models;

using THH.IdentityServer.Services;

using System;
using System.Collections.Generic;

namespace THH.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("resource_mainapi"){
                Scopes={"mainapi_fullpermission"},
                ApiSecrets=new []{ new Secret("mainapi_secret".Sha256())}},
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };
        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("mainapi_fullpermission","MAIN API ICIN TUM IZINLER"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new ProfileWithRoleIdentityResource(),
                new IdentityResources.Email(),
            };
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "WebClient_CC",
                    ClientName = "Web Client CC",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("webclient_client_secret".Sha256()) },

                    AllowedScopes =
                    {
                        IdentityServerConstants.LocalApi.ScopeName,
                    },
                     AccessTokenLifetime =(int)TimeSpan.FromDays(20).TotalSeconds
                },

                new Client
                {
                    ClientId = "WebClient_ROP",
                    ClientName = "Web Client ROP",

                    ClientSecrets = { new Secret("webclient_client_secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    AllowOfflineAccess = true,
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        IdentityServerConstants.LocalApi.ScopeName,
                        "mainapi_fullpermission",
                        "roles"
                    },
                    AccessTokenLifetime =(int)TimeSpan.FromDays(5).TotalSeconds,
                    RefreshTokenUsage=TokenUsage.ReUse,
                    RefreshTokenExpiration=TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime=(int)TimeSpan.FromDays(20).TotalSeconds
                },
                new Client
                {
                    ClientId = "Token_Exchange_Clinet",
                    ClientName = "Token Exchange Clinet",

                    AllowedGrantTypes = new []{"urn:ietf:params:oauth:grant-type:token-exchange"},
                    ClientSecrets = { new Secret("webclient_client_secret".Sha256()) },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        "mainapi_fullpermission"
                    }
                }
            };
    }

}