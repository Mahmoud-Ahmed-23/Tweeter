using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tweeter.Shared.ErrorModule.Exeptions
{
	public class UnAuthorizedException : ApplicationException
	{
		public UnAuthorizedException(string message) : base(message)
		{
		}
		public UnAuthorizedException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
