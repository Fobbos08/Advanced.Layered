using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer.Quickstart;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace IdentityServer
{
    public class ProfileService : IProfileService
    {
        public ProfileService() { }
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = Storage.Users.FirstOrDefault(x => x.Name + "id" == context.Subject.GetSubjectId());

            context.IssuedClaims.Add(new Claim("role", user.RoleName));
            return Task.CompletedTask;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            return Task.CompletedTask;
        }
    }
}
