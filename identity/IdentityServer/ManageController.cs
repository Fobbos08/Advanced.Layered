using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer.Quickstart;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageController : ControllerBase
    {
        private int counter = 0;

        [HttpPost]
        [Route("adduser")]
        public ActionResult AddUser(string name, string password)
        {
            Storage.Users.Add(new User() { Name = name, Password = password });
            Storage.TestUsers.Clear();
            Storage.TestUsers.AddRange(Storage.Users.Select(x => new TestUser()
            {
                SubjectId = x.Name + "id",
                Password = x.Password,
                Username = x.Name
            }));
            return Ok();
        }

        [HttpPost]
        [Route("addrole")]
        public ActionResult AddRole(string name)
        {
            Storage.Roles.Add(new Role() { Name = name });
            return Ok();
        }

        [HttpPost]
        [Route("setpermissions")]
        public ActionResult SetRolePermissions(string name, AccessRights permissions)
        {
            var item = Storage.Roles.FirstOrDefault(x => x.Name.Equals(name));

            if (item == null) return NotFound();

            item.Permissions = permissions;

            return Ok();
        }

        [HttpPost]
        [Route("setroletouser")]
        public ActionResult SetRoleToUser(string userName, string roleName)
        {
            var user = Storage.Users.FirstOrDefault(x => x.Name.Equals(userName));

            if (user == null) return NotFound();

            user.RoleName = roleName;

            Storage.TestUsers.Clear();
            Storage.TestUsers.AddRange(Storage.Users.Select(x => new TestUser()
            {
                SubjectId = x.Name + "id",
                Password = x.Password,
                Username = x.Name,
                Claims = x.RoleName == null ? new List<Claim>() : new List<Claim>(){ new Claim(JwtClaimTypes.Role, x.RoleName) }
            }));

            return Ok();
        }
    }
}
