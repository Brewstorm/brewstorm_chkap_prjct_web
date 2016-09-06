﻿using IdentityServer4.Models;
using System.Collections.Generic;

namespace CheckAppCore.Identity
{
    public class Scopes
    {
        public static IEnumerable<Scope> Get()
        {
            return new List<Scope>
            {
                StandardScopes.OpenId,
                StandardScopes.Profile,

                new Scope
                {
                    Name = "api1",
                    DisplayName = "API 1",
                    Description = "API 1 features and data",
                }
            };
        }
    }
}