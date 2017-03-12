using Ranaitfleur.Infrastructure.SagePayApi.Models;

namespace Ranaitfleur.Infrastructure.SagePayApi
{
    public interface ICryptography
    {
        string EncryptModel(SagePayCryptModel model);
        SagePayResponseModel DecryptModel(string crypt);
    }
}