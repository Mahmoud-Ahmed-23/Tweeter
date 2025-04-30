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

        }
    }
}
