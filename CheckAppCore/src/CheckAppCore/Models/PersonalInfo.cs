using CheckAppCore.Enumerators;

namespace CheckAppCore.Models
{
    public class PersonalInfo
    {
        public static string DEFAULT_PHOTO_URL = "/images/common/ic_account_circle_black.png";

        public int ID { get; set; }
        public int? UserID { get; set; }
        public User User { get; set; }
        public int? ProfessionalID { get; set; }
        public Professional Professional { get; set; }
        public string Name { get; set; }
        public string SrcPhoto { get; set; } = DEFAULT_PHOTO_URL;
        public GenderEnum GenderID { get; set; }
    }
}
