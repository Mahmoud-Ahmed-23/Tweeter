using Tweeter.Shared.Results;

namespace Tweeter.Core.Application.Bases
{
	public interface IBaseHandler
	{
		Task<Response<T>> HandleResultAsync<T>(Task<Result<T>> serviceResultTask);
	}
}
