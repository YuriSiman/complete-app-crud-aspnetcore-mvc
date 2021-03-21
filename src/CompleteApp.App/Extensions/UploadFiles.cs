using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace CompleteApp.App.Extensions
{
    public class UploadFiles
    {
        public async Task<bool> UploadImage(IFormFile arquivo, string imgPrefixo)
        {
            if (arquivo.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", imgPrefixo + arquivo.FileName);

            if (File.Exists(path)) return false;

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            return true;
        }
    }
}
