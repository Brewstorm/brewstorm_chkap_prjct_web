using CheckAppCore.Enumerators;

namespace CheckAppCore.Models
{
    public class UserRole
    {
        public Role Role { get; set; }
        public int RoleID { get; set; }
        public User User { get; set; }
        public int UserID { get; set; }
    }
}
