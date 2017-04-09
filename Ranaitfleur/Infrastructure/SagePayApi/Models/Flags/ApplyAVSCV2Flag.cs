namespace Ranaitfleur.Infrastructure.SagePayApi.Models
{
    public enum ApplyAVSCV2Flag
    {
        CheckIfAvsOrCv2Enabled = 0,
        ForceCheckAvsOrCv2WithRules = 1,
        ForceNoCheckAvsOrCv2 = 2,
        ForceCheckAvsOrCv2NoRules = 3
    }
}
