using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tweeter.Shared.ErrorModule.Exeptions
{
	public class NotFoundExeption : ApplicationException
	{
		public NotFoundExeption(string name, object key)
			: base($"{name} with Id : ({key} is not Found)")
		{

		}

	}
}
