using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tweeter.Shared.ErrorModule.Exeptions
{
	public class BadRequestExeption : ApplicationException
	{
		public BadRequestExeption(string message) : base(message)
		{
		}
		public BadRequestExeption(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
