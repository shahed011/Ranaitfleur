namespace Ranaitfleur.Infrastructure.SagePayApi.Models
{
    public class Card
    {
        public string CardType { get; set; }
        public string LastFourDigits { get; set; }
        public string ExpiryDate { get; set; }
        public string CardIdentifier { get; set; }
        public bool Reusable { get; set; }
    }
}
