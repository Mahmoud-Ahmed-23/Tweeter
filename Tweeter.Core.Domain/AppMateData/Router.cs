namespace Tweeter.Core.Domain.AppMateData
{
    public class Router
    {
        public const string root = "api";
        public const string version = "v1";
        public const string Rule = root + "/" + version + "/";
        public static class AccountRouting
        {
            public const string prefix = Rule + "Account";

            public const string Register = prefix + "/Register";
            public const string SendCode = prefix + "/SendCode";
            public const string VerifyCode = prefix + "/VerifyCode";

        }
        public static class AuthenticationRouting
        {
            public const string prefix = Rule + "Authentication";

            public const string Login = prefix + "/Login";
            public const string ResetPassword = prefix + "/ResetPassword";
            public const string ChangePassword = prefix + "/ChangePassword";
            public const string Logout = prefix + "/Logout";
            public const string GetCurrentUser = prefix + "/GetCurrentUser";

        }
        public static class ChatRouting
        {
            public const string prefix = Rule + "Chat";
            public const string messagid = "/{messageid}";

            public const string GetConversation = prefix + "/GetConversation";
            public const string SendMessage = prefix + "/SendMessage";
            public const string GetMessages = prefix + "/GetMessages";
            public const string DeleteMessage = prefix + "/DeleteMessage";
            public const string GetUnreadMessages = prefix + "/GetUnreadMessages";
            public const string MarkAsRead = prefix + "/MarkAsRead";


        }


    }
}
