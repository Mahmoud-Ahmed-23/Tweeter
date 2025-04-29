using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tweeter.Shared.Results
{
	public class Result
	{
		public bool IsSuccess { get; set; }
		public string ErrorMessage { get; set; }
		public ErrorType ErrorType { get; set; }

		public static Result Success()
			=> new Result { IsSuccess = true, ErrorType = ErrorType.None };

		public static Result Fail(string errorMessage, ErrorType errorType = ErrorType.Unexpected)
			=> new Result { IsSuccess = false, ErrorMessage = errorMessage, ErrorType = errorType };
	}

	public class Result<T> : Result
	{
		public T Data { get; set; }

		public static Result<T> Success(T data)
			=> new Result<T> { IsSuccess = true, Data = data, ErrorType = ErrorType.None };

		public static Result<T> Fail(string errorMessage, ErrorType errorType = ErrorType.Unexpected)
			=> new Result<T> { IsSuccess = false, ErrorMessage = errorMessage, ErrorType = errorType };
	}

}
