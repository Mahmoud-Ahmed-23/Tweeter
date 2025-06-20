using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Domain.Contracts.Infrastructure;

namespace Tweeter.Infrastructure.AttachementServices
{
	internal class AttachmentService : IAttachmentService
	{
		private readonly List<string> _allowedExtentions = new() { ".png", ".jpg", ".jpeg", ".PNG", ".JPG", ".JPEG" };
		private const int _allowedMaxSize = 2_097_152;
		public async Task<string?> UploadAsynce(IFormFile file, string folderName)
		{
			var extention = Path.GetExtension(file.FileName);

			if (!_allowedExtentions.Contains(extention))
				return null;

			if (file.Length > _allowedMaxSize)
				return null;

			var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", folderName);
			if (!Directory.Exists(folderPath))
				Directory.CreateDirectory(folderPath);

			var fileName = $"{Guid.NewGuid()}{extention}";
			var filePath = Path.Combine(folderPath, fileName);

			using var fileStream = new FileStream(filePath, FileMode.Create);
			await file.CopyToAsync(fileStream);

			return $"images/{folderName}/{fileName}";
		}

		public bool Delete(string filePath)
		{
			if (File.Exists(filePath))
			{
				File.Delete(filePath);
				return true;
			}
			return false;
		}
	}
}
