using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;

namespace BeanSceneSystem.Services
{
    public class FileServiceOuter
    {
        public FileServiceInner Inner { get; set; }

        public FileServiceOuter(IWebHostEnvironment env)
        {
            this.Inner = new FileServiceInner(env);
        }

        public class FileServiceInner : IFileService
        {
            private readonly IWebHostEnvironment _environment;

            public FileServiceInner(IWebHostEnvironment env)
            {
                _environment = env;
            }

            public Tuple<int, string> SaveImage(IFormFile imageFile)
            {
                try
                {
                    var wwwPath = _environment.WebRootPath;
                    var path = Path.Combine(wwwPath, "Uploads");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    // Check the allowed extensions
                    var ext = Path.GetExtension(imageFile.FileName);
                    var allowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };
                    if (!allowedExtensions.Contains(ext))
                    {
                        string msg = string.Format("Only {0} extensions are allowed", string.Join(",", allowedExtensions));
                        return new Tuple<int, string>(0, msg);
                    }
                    string uniqueString = Guid.NewGuid().ToString();
                    var newFileName = uniqueString + ext;
                    var fileWithPath = Path.Combine(path, newFileName);
                    using var stream = new FileStream(fileWithPath, FileMode.Create);
                    imageFile.CopyTo(stream);
                    return new Tuple<int, string>(1, newFileName);
                }
                catch (Exception)
                {
                    return new Tuple<int, string>(0, "Error has occurred");
                }
            }

            public bool DeleteImage(string imageFileName)
            {
                try
                {
                    var wwwPath = _environment.WebRootPath;
                    var path = Path.Combine(wwwPath, "Uploads\\", imageFileName);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }

    public interface IFileService
    {
        Tuple<int, string> SaveImage(IFormFile imageFile);
        bool DeleteImage(string imageFileName);
    }
}

