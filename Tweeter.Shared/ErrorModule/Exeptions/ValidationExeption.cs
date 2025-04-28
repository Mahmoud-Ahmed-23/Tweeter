using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tweeter.Shared.ErrorModule.Exeptions
{
	public class ValidationExeption : BadRequestExeption
	{
		public required IEnumerable<string> Errors { get; set; }

		public ValidationExeption(string message = "Bad Request") : base(message)
		{
		}
		public ValidationExeption(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
