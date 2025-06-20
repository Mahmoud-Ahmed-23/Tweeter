using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Tweeter.Core.Domain.Contracts.Infrastructure
{
	public interface IAttachmentService
	{
		Task<string?> UploadAsynce(IFormFile file, string folderName);

		bool Delete(string filePath);
	}
}
