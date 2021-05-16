// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
	public static class Config
	{
		public static IEnumerable<IdentityResource> IdentityResources =>
			new IdentityResource[]
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Profile(),
				new IdentityResource{
					Name = "custom1",
					Description = "custom1 description",
					DisplayName = "custom1 displayname",
					Enabled = true,
					UserClaims = new List<string>{
						"custom1_claim1",
						"custom1_claim2",
						"custom1_claim3",
					}
				}
			};

		public static IEnumerable<ApiScope> ApiScopes =>
			new ApiScope[]
			{
				new ApiScope("scope1"),
				new ApiScope("scope2"),
				new ApiScope("api1"),
				new ApiScope("custom1_scope", "custom1_scope_description",new List<string>{
					"custom1_scope_claim_1",
					"custom1_scope_claim_2",
					"custom1_scope_claim_3",
					"custom1_scope_claim_4"
				}),
			};

		public static IEnumerable<Client> Clients =>
			new Client[]
			{
                // m2m client credentials flow client
                new Client
				{
					ClientId = "m2m.client",
					ClientName = "Client Credentials Client",

					AllowedGrantTypes = GrantTypes.ClientCredentials,
					ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

					AllowedScopes = { "scope1" }
				},

                // interactive client using code flow + pkce
                new Client
				{
					ClientId = "interactive",
					ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

					AllowedGrantTypes = GrantTypes.Code,

					RedirectUris = { "https://localhost:8001/signin-oidc" },
					FrontChannelLogoutUri = "https://localhost:8001/signout-oidc",
					PostLogoutRedirectUris = { "https://localhost:8001/signout-callback-oidc" },

					AllowOfflineAccess = true,
					AllowedScopes = new List<string>
			{
				IdentityServerConstants.StandardScopes.OpenId,
				IdentityServerConstants.StandardScopes.Profile,
				"custom1_scope",
				"api1"
			}
				},

				// JavaScript Client
new Client
{
	ClientId = "js",
	ClientName = "JavaScript Client",
	AllowedGrantTypes = GrantTypes.Code,
	RequireClientSecret = false,

	RedirectUris =           { "https://localhost:5003/callback.html" },
	PostLogoutRedirectUris = { "https://localhost:5003/index.html" },
	AllowedCorsOrigins =     { "https://localhost:5003" },

	AllowedScopes =
	{
		IdentityServerConstants.StandardScopes.OpenId,
		IdentityServerConstants.StandardScopes.Profile,
		"api1"
	}
}
			};
	}
}