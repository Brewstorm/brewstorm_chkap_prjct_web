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

        public bool AuthenticateUser(string email, string password)
        {
            if (string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password)) 
                return false;

            return Context.Users.Any(o => o.EmailAddress == email && o.Password == password);
        }

        public bool AuthenticateFBUser(string userid)
        {
            if (string.IsNullOrEmpty(userid))
                return false;

            return Context.Users.Any(o => o.FacebookID == userid);
        }

        public Task<bool> RegisterUser(RegisterModel registerModel)
        {
            try
            {
                var user = new User
                {
                    Password = registerModel.Password,
                    FacebookID = registerModel.FacebookID,
                    EmailAddress = registerModel.Email,
                    UserRoles = new List<UserRole>
                    {
                        new UserRole { RoleID = registerModel.UserType.GetHashCode() }
                    },
                    PersonalInfo = new PersonalInfo()
                    {
                        Name = registerModel.Name,
                        SrcPhoto = registerModel.PhotoUrl
                    }
                };

                Context.Users.Add(user);
                Context.SaveChanges();

                if(registerModel.UserType == UserType.Professional)
                {
                    return Task.FromResult(new ProfessionalRepository(Context).RegisterProfessional(registerModel));
                }

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
            return Context.Users
                            .Include("UserRoles")
                            .Include("PersonalInfo")
                            .FirstOrDefaultAsync(o => o.FacebookID == subject || o.EmailAddress == subject);
        }
    }
}
