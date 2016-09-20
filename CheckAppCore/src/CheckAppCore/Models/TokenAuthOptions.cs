namespace CheckAppCore.Models
{
    public class TokenAuthOptions
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public object SigningCredentials { get; set; }
    }
}