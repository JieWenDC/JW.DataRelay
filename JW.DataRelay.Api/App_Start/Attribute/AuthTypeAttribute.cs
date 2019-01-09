using JW.DataRelay.Enum;

namespace JW.DataRelay.Api.App_Start.Attribute
{
    public class AuthTypeAttribute : System.Attribute
    {
        public DataCollectionRightAuthTypeEnum ActionAuthType { get; set; }

        public AuthTypeAttribute(DataCollectionRightAuthTypeEnum dataCollectionRightAuthTypeEnum)
        {
            ActionAuthType = dataCollectionRightAuthTypeEnum;
        }
    }
}