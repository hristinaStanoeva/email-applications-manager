namespace EMS.Data
{
    public static class Constants
    {
        // DB records
        internal const string RoleManager = "manager";
        internal const string RoleOperator = "operator";
        internal const string DefaultManagerUsername = "manager@ems.com";
        internal const string DefaultOperator1Username = "operator1@ems.com";
        internal const string DefaultOperator2Username = "operator2@ems.com";
        internal const string DefaultPassword = "Password-123";

        // Messages
        public const string SuccAppCreate = "You have successfully created an application";        
        public const string SuccAppValid = "You have approved the application";
        public const string SuccAppInvalid = "You have rejected the application";
        public const string SuccAppNew = "You have marked an appication as New";

        public const string SuccEmailInvalid = "You have marked an email as Invalid";
        public const string SuccEmailNew = "You have marked an email as New";


        public const string PassValidErrMsg = "The {0} must be at least {2} and at max {1} characters long.";
        public const string PassDisplayName = "Password";
        public const string PassConfirm = "Confirm password";
        public const string PassMatchMsg = "The password and confirmation password do not match.";

        public const string EmailDisplayName = "Email";

        // Navigation
        public const string PageIndex = "Index";
        public const string PageOpen = "Open";
        public const string PageEmail = "Email";
        public const string PageHome = "Home";
        public const string ChangePassRedirect = "/Identity/Account/Login";

        public const string HeaderReferer = "Referer";


        //Tabs
        public const string TabAll = "all";
        public const string TabNew = "new";
        public const string TabOpen = "open";
        public const string TabClosed = "closed";

        // Users
        public const string SelListTextManager = "Manager";
        public const string SelListValueManager = "manager";
        public const string SelListTextOperator = "Operator";
        public const string SelListValueOperator = "operator";

        public const string Manager = "manager";
        public const string Operator = "operator";


        // Misc
        public const string TempDataMsg = "message";
        public const string AuthPolicy = "IsPasswordChanged";

        public const string TimeParser0Min = "0 min.";
        public const string TimeParserMin = " min.";
        public const string TimeParserHrsMin = " hrs. and";
        public const string TimeParserDays = " days, ";

        // Logger
        public const string LogEmailInvalid = "{0} has marked email {1} as invalid";
        public const string LogEmailNew = "{0} has marked email {1} as new";

    }
}
