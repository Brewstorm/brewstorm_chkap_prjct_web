using System.Linq;
using System.Threading.Tasks;
using CheckAppCore.Data;
using CheckAppCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using CheckAppCore.Enumerators;
using System.Collections.Generic;

namespace CheckAppCore.Repositories
{
    public class UserRepository : EntityFrameworkRepository<CheckAppContext, User>
    {
        public UserRepository(CheckAppContext context) : base(context)
        {
        }

        public Task<bool> AuthenticateUser(string email, string password)
        {
            return Context.Users.AnyAsync(o => o.EmailAddress == email && o.Password == password);
        }

        public Task<bool> AuthenticateFBUser(string userid)
        {
            return Context.Users.AnyAsync(o => o.FacebookID == userid);
        }

        public Task<bool> RegisterUser(RegisterModel registerModel)
        {
            try
            {
                var user = new User
                {
                    FirstName = registerModel.Name,
                    LastName = registerModel.LastName,
                    Password = string.Empty,
                    PhotoUrl = registerModel.PhotoUrl,
                    FacebookID = registerModel.FacebookID,
                    EmailAddress = registerModel.Email
                };   

                var userRole = new UserRole { User = user, RoleID = registerModel.UserType };
                user.UserRoles = new List<UserRole>() { userRole };

                Context.Users.Add(user);
                Context.SaveChanges();

                return Task.FromResult(true);
            }
            catch(Exception)
            {
                return Task.FromResult(false);
            }
        }

        public Task<User> GetUserFromEmail(string email)
        {
            return Context.Users.FirstOrDefaultAsync(o => o.EmailAddress == email);
        }

        public Task<User> GetUserFromFacebookID(string fb_id)
        {
            return Context.Users.FirstOrDefaultAsync(o => o.FacebookID == fb_id);
        }

        public Task<User> GetUserFromEmailOrOauthID(string subject)
        {
            return Context.Users.FirstOrDefaultAsync(o => o.FacebookID == subject || o.EmailAddress == subject);
        }
    }
}
