namespace HR.Project.WebUI.Areas.Employee.Models
{
    public class SpendingDocumentPreview
    {
        public static void ShowPreview(IFormFile file, HttpResponse response, IWebHostEnvironment environment)
        {
            if (file != null && file.Length > 0)
            {
                // Yüklenen dosya tipini doğrula

                if (file.ContentType.Contains("pdf"))
                {
                    // Dosyayı bellekte tut

                    var buffer = new byte[file.Length];
                    file.OpenReadStream().Read(buffer, 0, (int)file.Length);

                    // Bellekteki dosyayı PDF olarak görüntüle

                    response.ContentType = "application/pdf";
                    response.Body.Write(buffer, 0, buffer.Length);

                    // PDF dosyasını wwwroot/Documents klasörüne kaydet

                    string folderName = "Documents";
                    string webRootPath = environment.WebRootPath;
                    string newPath = Path.Combine(webRootPath, folderName);
                    if (!Directory.Exists(newPath))
                    {
                        Directory.CreateDirectory(newPath);
                    }
                    string fullPath = Path.Combine(newPath, file.FileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        stream.Write(buffer, 0, buffer.Length);
                    }
                }
                else
                {
                    throw new Exception("Dosya PDF olmalıdır.");
                }
            }
            else
            {
                throw new Exception("Lütfen dosya seçiniz.");
            }
        }
    }
}
