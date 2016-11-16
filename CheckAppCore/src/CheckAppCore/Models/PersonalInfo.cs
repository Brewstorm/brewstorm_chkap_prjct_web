using CheckAppCore.Enumerators;

namespace CheckAppCore.Models
{
    public class PersonalInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string SrcPhoto { get; set; }
        public GenderEnum GenderID { get; set; }
    }
}
