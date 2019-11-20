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
        public const string SuccStatusInvalid = "You have marked an email as Invalid";
        public const string SuccStatusNotReviewed = "You have marked an email as Not Reviewed";
        public const string SuccStatusNew = "You have marked an email as New";
        public const string SuccAppValid = "You have approved the application";
        public const string SuccAppInvalid = "You have rejected the application";
        public const string SuccAppNew = "You have marked an appication as New";

        // Navigation
        public const string PageIndex = "Index";
        public const string PageOpen = "Open";
        public const string PageEmail = "Email";
        public const string PageHome = "Home";
        public const string ChangePassRedirect = "/Identity/Account/Login";


        //Tabs
        public const string TabAll = "all";
        public const string TabNew = "new";
        public const string TabOpen = "open";
        public const string TabClosed = "closed";

        // Roles
        public const string SelListTextManager = "Manager";
        public const string SelListValueManager = "manager";
        public const string SelListTextOperator = "Operator";
        public const string SelListValueOperator = "operator";

        // Misc
        public const string TempDataMsg = "message";
        public const string AuthPolicy = "IsPasswordChanged";
    }
}
