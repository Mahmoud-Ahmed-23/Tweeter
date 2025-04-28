using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tweeter.Shared.ErrorModule.Exeptions
{
	public class UnAuthorizedExeption : ApplicationException
	{
		public UnAuthorizedExeption(string message) : base(message)
		{
		}
		public UnAuthorizedExeption(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
