using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace BLL.Services.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        public AttachmentService(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
        }

        private readonly string[] AllowedExtensions = { ".jpg" , "jpeg" , ".png" };
        private readonly long MaxSize = 5 * 1024 * 1024;
        private readonly IWebHostEnvironment webHost;

        public string? Upload(string folderName, IFormFile file)
        {
            try
            {
                if (folderName is null || file is null || file.Length == 0) return null;
                //1.Check Extension
                var ex = Path.GetExtension(file.FileName).ToLower();
                if (!AllowedExtensions.Contains(ex)) return null;
                //2.Check Size
                if (file.Length > MaxSize) return null;
                //3.Get Located Folder Path
                var FolderPath = Path.Combine(webHost.WebRootPath, "images", folderName);
                if (!Directory.Exists(FolderPath))
                {
                    Directory.CreateDirectory(FolderPath);
                }
                //4.Make Attachment Name Unique-- GUID
                var FileName = Guid.NewGuid().ToString() + ex;
                //5.Get File Path
                var FilePath = Path.Combine(FolderPath, FileName);
                //6.Create File Stream To Copy File[Unmanaged]
                using var FileStream = new FileStream(FilePath, FileMode.Create);
                //7.Use Stream To Copy File
                file.CopyTo(FileStream);
                //8.Return FileName To Store In Database
                return FileName;
            }
            catch (Exception ex) {

                Console.WriteLine($"Field To Upload : {ex}");
                return null;
            }
        } 
        public bool Delete(string folderName, string fileName)
        {
            try
            {
                if(string.IsNullOrEmpty(folderName) || string.IsNullOrEmpty(fileName)) return false;

                var fullPath = Path.Combine(webHost.WebRootPath, "images", folderName , fileName);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Field To delete : {ex}");
                return false;
            }
        }


    }
}
