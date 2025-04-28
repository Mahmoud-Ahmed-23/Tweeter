using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		}
		public static class AuthenticationRouting
		{
			public const string prefix = Rule + "Account";

			public const string Login = prefix + "/Login";
		}
	}
}
