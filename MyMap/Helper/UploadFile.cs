using Microsoft.AspNetCore.Mvc;
using MyMap.Models.identity;
using static MyMap.Controllers.MapController;

namespace MyMap.Helper
{
    public interface IUploadFile
    {
        string UploadData(UploadFileModel uploadFile);
        string UploadDataWithOriginalFileName(UploadFileModel uploadFile);
        string getFileContentType(string FileName);
    }

    public class UploadFile : IUploadFile
    {

        public UploadFile()
        {

        }

        public string UploadData(UploadFileModel uploadFile)
        {
            if (uploadFile.file != null)
            {
                if (uploadFile.file.Length <= 0) return null;
                string path = System.IO.Path.Combine(@"wwwroot/", uploadFile.folder);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var ext = Path.GetExtension(uploadFile.file.FileName).ToLower();
                var fileName = DateTime.Now.ToString("yyyyMMdd") + "_" + DateTime.Now.Ticks + Path.GetExtension(uploadFile.file.FileName).ToLower();
                var filePath = Path.Combine(path, fileName);

                try
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        uploadFile.file.CopyTo(stream);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return null;
                }
                return fileName;
            }
            else
            {
                return null;
            }
        }

        public string UploadDataWithOriginalFileName(UploadFileModel uploadFile)
        {
            if (uploadFile.file != null)
            {
                if (uploadFile.file.Length <= 0) return null;
                string path = Path.Combine(@"wwwroot/", uploadFile.folder);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var filePath = Path.Combine(path, uploadFile.file.FileName);

                try
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        uploadFile.file.CopyTo(stream);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return null;
                }
                return uploadFile.file.FileName;
            }

            return null;
        }

        /**
         從附檔名取得檔案型態
             */
        public string getFileContentType(string FileName)
        {
            var extension = Path.GetExtension(FileName);
            string typeS;
            switch (extension.ToLower())
            {
                case ".doc":
                case ".dot":
                    typeS = "application/msword";
                    break;
                case ".docx":
                    typeS = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    break;
                case ".dotx":
                    typeS = "application/vnd.openxmlformats-officedocument.wordprocessingml.template";
                    break;
                case ".docm":
                    typeS = "application/vnd.ms-word.document.macroEnabled.12";
                    break;
                case ".dotm":
                    typeS = "application/vnd.ms-word.template.macroEnabled.12";
                    break;
                case ".xls":
                case ".xlt":
                case ".xla":
                    typeS = "application/vnd.ms-excel";
                    break;
                case ".ppt":
                case ".pot":
                case ".pps":
                case ".ppa":
                    typeS = "application/vnd.ms-powerpoint";
                    break;
                case ".xlsx":
                    typeS = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;
                case ".mdb":
                    typeS = "application/vnd.ms-access";
                    break;
                case ".mp3":
                    typeS = "audio/mpeg3";
                    break;
                case ".gif":
                    typeS = "image/gif";
                    break;
                case ".png":
                    typeS = "image/png";
                    break;
                case ".tif":
                    typeS = "image/tif";
                    break;
                case ".bmp":
                    typeS = "image/bmp";
                    break;
                case ".jpg":
                case "jpeg":
                    typeS = "image/jpeg";
                    break;
                case ".wav":
                    typeS = "audio/wav";
                    break;
                case ".pdf":
                    typeS = "application/pdf";
                    break;
                default:
                    typeS = "text/plain";
                    break;
            }

            return typeS;
        }
    }
}




