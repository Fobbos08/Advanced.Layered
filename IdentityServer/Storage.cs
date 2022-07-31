﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Test;

namespace IdentityServer
{
    [Flags]
    public enum AccessRights
    {
        Read = 1,
        Create = 2,
        Update = 4,
        Delete = 8
    }

    public class User
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public string RoleName { get; set; }
    }

    public class Role
    {
        public string Name { get; set; }

        public AccessRights Permissions { get; set; }
    }

    public static class Storage
    {
        public static List<Role> Roles = new List<Role>()
        {
            new Role()
            {
                Name = "manager",
                Permissions = AccessRights.Create | AccessRights.Delete | AccessRights.Read | AccessRights.Update
            }
        };

        public static List<User> Users = new List<User>()
        {
            new User()
            {
                Name = "user1",
                Password = "pass",
                RoleName = "manager"
            }
        };

        public static List<TestUser> TestUsers
        {
            get
            {
                return Users.Select(x => new TestUser()
                {
                    SubjectId = x.Name+"id",
                    Password = x.Password,
                    Username = x.Name,
                    Claims = {new Claim(JwtClaimTypes.Role, x.RoleName)}
                }).ToList();
            }
        }
    }
}