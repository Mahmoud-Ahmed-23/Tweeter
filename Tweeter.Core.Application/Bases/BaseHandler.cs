using Tweeter.Shared.Results;

namespace Tweeter.Core.Application.Bases
{
    public class BaseHandler : ResponseHandler, IBaseHandler
    {


        public async Task<Response<T>> HandleResultAsync<T>(Task<Result<T>> serviceResultTask)
        {
            var result = await serviceResultTask;

            if (!result.IsSuccess)
                return Fail<T>(result.ErrorMessage, result.ErrorType);

            return Success(result.Data, Meta: result.Count!);
        }
    }


}
