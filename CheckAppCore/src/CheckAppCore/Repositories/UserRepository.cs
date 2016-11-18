using System.Linq;
using System.Threading.Tasks;
using CheckAppCore.Data;
using CheckAppCore.Models;
using Microsoft.EntityFrameworkCore;

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
    }
}
