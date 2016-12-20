namespace Ranaitfleur.Infrastructure.SagePayApi.Models
{
    public class Error
    {
        public int Code { get; set; }
        public string Property { get; set; }
        public string Description { get; set; }
        public string ClientMessage { get; set; }
    }
}