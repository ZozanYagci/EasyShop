using Core.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    // dosya yükleme, silme, güncelleme gibi işlemler burada yapılacak.
    // burada locale yüklüyoruz fakat, Azure Blob, AWS S3, FTP gibi faklı sistemler kullanılacaksa IFileService i implemente eden bir sınıf oluşturursun.
    public class LocalFileManager : IFileService
    {
        private readonly IPathProvider _pathProvider;

        public LocalFileManager(IPathProvider pathProvider)
        {
            _pathProvider = pathProvider;
        }

        public async Task<bool> DeleteAsync(string relativePath)
        {
            try
            {
                // wwwroot içindeki dosyanın tam yolunu oluşturalım.
                string fullPath = Path.Combine(_pathProvider.GetRootPath(), relativePath);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);

                }
                return await Task.FromResult(true);  // TODO: logger
            }
            catch
            {
                return await Task.FromResult(false);
            }

        }

        public async Task<string> UploadAsync(IFormFile file, string subFolder)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Dosya geçersiz");


            string folderPath = Path.Combine(_pathProvider.GetRootPath(), "easy-shop-template", "uploads", subFolder);
            Directory.CreateDirectory(folderPath); //klasör yoksa oluştur


            // dosya isimleri olası çakışmalar için benzersiz olsun
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var fullPath = Path.Combine(folderPath, uniqueFileName);


            //dosyayı fiziksel olarak sunucuya kaydet
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            string relativePath = Path.Combine("easy-shop-template", "uploads", subFolder, uniqueFileName).Replace("\\", "/");
            return relativePath;
        }

    }
}
