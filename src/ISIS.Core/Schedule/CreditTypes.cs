using System.ComponentModel;

namespace ISIS.Schedule
{
    public enum CreditTypes
    {

        [Description("Contract Training Funded")]
        ContractTrainingFunded = 1,

        [Description("Contract Training Non-Funded")]
        ContractTrainingNonFunded = 2,

        [Description("Special Interests Funded")]
        SpecialInterests = 3,

        [Description("Grant Funded")]
        GrantFunded = 4,

        [Description("Grant Non-Funded")]
        GrantNonFunded = 5,

        [Description("Workforce Funded")]
        WorkforceFunded = 6,

        [Description("Workforce Non-Funded")]
        WorkforceNonFunded = 7
        
    }
}
