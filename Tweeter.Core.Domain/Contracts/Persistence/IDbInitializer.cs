using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tweeter.Core.Domain.Contracts.Persistence
{
	public interface IDbInitializer
	{
		Task InitializeAsync();
	}
}
