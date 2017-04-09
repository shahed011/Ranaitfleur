namespace Ranaitfleur.Infrastructure.SagePayApi.Models
{
    public enum SendMailFlag
    {
        DoNotSend = 0,
        SendToCustomerAndVendor = 1,
        SendToVendorButNotToCustomer = 2
    }
}
