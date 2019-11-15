namespace EMS.Data.Seed
{
    public static class Constants
    {
        // DB records
        internal const string roleManager = "manager";
        internal const string roleOperator = "operator";
        internal const string defaultManagerUsername = "manager@ems.com";
        internal const string defaultOperator1Username = "operator1@ems.com";
        internal const string defaultOperator2Username = "operator2@ems.com";
        internal const string defaultPassword = "Password-123";

        public const string SuccAppCreate = "You have successfully created an application";
        public const string SuccStatusInvalid = "You have marked an email as Invalid";
        public const string SuccStatusNotReviewed = "You have marked an email as Not Reviewed";
        public const string SuccStatusNew = "You have marked an email as New";
        public const string SuccAppValid = "You have marked an application as Valid";
        public const string SuccAppInvalid = "You have marked an application as Invalid";
    }
}
