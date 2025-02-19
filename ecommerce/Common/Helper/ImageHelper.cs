using SharpCompress.Common;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using static Humanizer.In;

namespace Common.Helper
{
    public class ImageHelper
    {
        static string[] mediaExtensions = { ".AVI", ".MP4", ".DIVX", ".WMV" };

        static bool IsMediaFile(string path)
        {
            return -1 != Array.IndexOf(mediaExtensions, Path.GetExtension(path).ToUpperInvariant());
        }

        public static string SaveFile(string fileBase64, string directory, string origindirectory)
        {
            try
            {
                string filePath = string.Empty;//data:application/octet-stream;base64,
                fileBase64 = Regex.Replace(fileBase64, @"data:font/ttf;base64,", String.Empty);
                fileBase64 = Regex.Replace(fileBase64, @"data:application/octet-stream;base64,", String.Empty);
                byte[] bytes = Convert.FromBase64String(fileBase64);

                string timee = DateTime.Now.ToString("yymmddhhmmssfff") + ".ttf";
                filePath = "wwwroot" + directory + timee;
                if (!Directory.Exists("wwwroot" + directory))
                {
                    DirectoryInfo di = Directory.CreateDirectory(filePath);
                }
                File.WriteAllBytes(filePath, bytes);
                filePath = origindirectory + timee;

                return filePath;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                return string.Empty;
            }
        }
        public static string SaveFilePdf(string fileBase64, string directory, string origindirectory)
        {
            try
            {
                string filePath = string.Empty;//data:application/octet-stream;base64,
                fileBase64 = Regex.Replace(fileBase64, @"data:application/pdf;base64,", String.Empty);
                fileBase64 = Regex.Replace(fileBase64, @"data:application/octet-stream;base64,", String.Empty);
                byte[] bytes = Convert.FromBase64String(fileBase64);
                string timee = DateTime.Now.ToString("yymmddhhmmssfff") + ".pdf";
                filePath = "wwwroot" + directory + timee;
                if (!Directory.Exists("wwwroot" + directory))
                {
                    DirectoryInfo di = Directory.CreateDirectory(filePath);
                }
                File.WriteAllBytes(filePath, bytes);
                filePath = origindirectory + timee;

                return filePath;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                return string.Empty;
            }
        }
        public static string SaveImage(string imageBase64, string directory, FileType fileType, string origindirectory)
        {
            try
            {
                string filePath = string.Empty;
                if (fileType == FileType.mp4)
                {
                    imageBase64 = Regex.Replace(imageBase64, @"^data:video\/[a-zA-Z0-9]+;base64,", String.Empty);
                    byte[] bytes = Convert.FromBase64String(imageBase64);
                    string timee = DateTime.Now.ToString("yymmddhhmmssfff") + ".mp4";
                    filePath = "wwwroot" + directory + timee;
                    if (!Directory.Exists("wwwroot" + directory))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(filePath);
                    }
                    File.WriteAllBytes(filePath, bytes);
                    //resizeImage.Save(Path.GetFullPath(filePath), new JpegEncoder() { Quality = 75 });
                    filePath = origindirectory + timee;
                }
                else
                {
                    imageBase64 = Regex.Replace(imageBase64, @"^data:image\/[a-zA-Z]+;base64,", String.Empty);
                    byte[] bytes = Convert.FromBase64String(imageBase64);
                    if (!String.IsNullOrEmpty(imageBase64))
                    {
                        using (Image<Rgba32> resizeImage = Image.Load<Rgba32>(bytes))
                        {
                            var targetSize = PrepareSizeImage(resizeImage);
                            //resizeImage.Mutate(x => x
                            //     .Resize(targetSize.Item1, targetSize.Item2)
                            // );
                            switch (fileType)
                            {

                                case FileType.JPeg:
                                    resizeImage.Mutate(x => x
                                         .BackgroundColor(Color.White)
                                     );
                                    string time = DateTime.Now.ToString("yymmddhhmmssfff") + ".jpg";
                                    filePath = "wwwroot" + directory + time;
                                    if (!Directory.Exists("wwwroot" + directory))
                                    {
                                        DirectoryInfo di = Directory.CreateDirectory(filePath);
                                    }
                                    resizeImage.Save(Path.GetFullPath(filePath), new JpegEncoder() { Quality = 75 });
                                    filePath = origindirectory + time;
                                    break;
                                case FileType.Png:
                                    string tims = DateTime.Now.ToString("yymmddhhmmssfff") + ".png";
                                    filePath = "wwwroot" + directory + tims;
                                    if (!Directory.Exists("wwwroot" + directory))
                                    {
                                        DirectoryInfo di = Directory.CreateDirectory(filePath);
                                    }
                                    resizeImage.Save(Path.GetFullPath(filePath), new PngEncoder());
                                    filePath = origindirectory + tims;
                                    break;
                            }
                        }
                    }
                }
                return filePath;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                return string.Empty;
            }
        }

        public Responses.BaseResponse SaveImageReturnSize(string imageBase64, string directory, FileType fileType, string origindirectory)
        {
            try
            {
                var width = 0;
                var height = 0;
                string filePath = string.Empty;
                imageBase64 = Regex.Replace(imageBase64, @"^data:image\/[a-zA-Z]+;base64,", String.Empty);
                byte[] bytes = Convert.FromBase64String(imageBase64);
                if (!String.IsNullOrEmpty(imageBase64))
                {
                    using (Image<Rgba32> resizeImage = Image.Load<Rgba32>(bytes))
                    {
                        switch (fileType)
                        {

                            case FileType.JPeg:
                                resizeImage.Mutate(x => x
                                     .BackgroundColor(Color.White)
                                 );
                                string time = DateTime.Now.ToString("yymmddhhmmssfff") + ".jpg";
                                filePath = "wwwroot" + directory + time;
                                if (!Directory.Exists("wwwroot" + directory))
                                {
                                    DirectoryInfo di = Directory.CreateDirectory(filePath);
                                }
                                resizeImage.Save(Path.GetFullPath(filePath), new JpegEncoder() { Quality = 75 });
                                filePath = origindirectory + time;
                                break;
                            case FileType.Png:
                                string tims = DateTime.Now.ToString("yymmddhhmmssfff") + ".png";
                                filePath = "wwwroot" + directory + tims;
                                if (!Directory.Exists("wwwroot" + directory))
                                {
                                    DirectoryInfo di = Directory.CreateDirectory(filePath);
                                }
                                resizeImage.Save(Path.GetFullPath(filePath), new PngEncoder());
                                filePath = origindirectory + tims;
                                break;
                        }
                        width = resizeImage.Width;
                        height = resizeImage.Height;
                    }
                }
                return new Responses.BaseResponse().Success(new
                {
                    filePath = ConnectionStrings.ImageDomain + filePath,
                    width = width,
                    height = height
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                return new Responses.BaseResponse().Fail(ex.Message);
            }
        }

        public static string SaveImageNew(string imageBase64, int type)
        {
            try
            {
                string directory = "";
                string origindirectory = "";
                var fileType = GetFileExtension(imageBase64);
                if (type == 1)
                {
                    directory = "/media/NewsImage/";
                    origindirectory = "/media/NewsImage/";
                }
                else if (type == 2)
                {
                    directory = "/media/Banner/";
                    origindirectory = "/media/Banner/";
                }
                else if (type == 3)
                {
                    directory = "/media/Product/";
                    origindirectory = "/media/Product/";
                }
                else if (type == 4)
                {
                    directory = "/media/Category/";
                    origindirectory = "/media/Category/";
                }
                else if (type == 5)
                {
                    directory = "/media/SaleChannel/";
                    origindirectory = "/media/SaleChannel/";
                }
                else if (type == 6)
                {
                    directory = "/media/Privilege/";
                    origindirectory = "/media/Privilege/";
                }
                else
                {
                    directory = "/media/Stampcard/";
                    origindirectory = "/media/Stampcard/";
                }
                string filePath = string.Empty;
                if (fileType == "mp4")
                {
                    imageBase64 = Regex.Replace(imageBase64, @"^data:video\/[a-zA-Z0-9]+;base64,", String.Empty);
                    byte[] bytes = Convert.FromBase64String(imageBase64);
                    string timee = DateTime.Now.ToString("yymmddhhmmssfff") + ".mp4";
                    filePath = "wwwroot" + directory + timee;
                    if (!Directory.Exists("wwwroot" + directory))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(filePath);
                    }
                    File.WriteAllBytes(filePath, bytes);
                    filePath = origindirectory + timee;
                }
                else
                {
                    imageBase64 = Regex.Replace(imageBase64, @"^data:image\/[a-zA-Z]+;base64,", String.Empty);
                    byte[] bytes = Convert.FromBase64String(imageBase64);
                    if (!String.IsNullOrEmpty(imageBase64))
                    {
                        using (Image<Rgba32> resizeImage = Image.Load<Rgba32>(bytes))
                        {
                            var targetSize = PrepareSizeImage(resizeImage);
                            resizeImage.Mutate(x => x
                                 .Resize(targetSize.Item1, targetSize.Item2)
                                 .BackgroundColor(Color.White)
                             );
                            switch (fileType)
                            {

                                case ".jpg":
                                    string time = DateTime.Now.ToString("yymmddhhmmssfff") + ".jpg";
                                    filePath = "wwwroot" + directory + time;
                                    if (!Directory.Exists("wwwroot" + directory))
                                    {
                                        DirectoryInfo di = Directory.CreateDirectory(filePath);
                                    }
                                    resizeImage.Save(Path.GetFullPath(filePath), new JpegEncoder() { Quality = 75 });
                                    filePath = origindirectory + time;
                                    break;
                                case ".png":
                                    string tims = DateTime.Now.ToString("yymmddhhmmssfff") + ".png";
                                    filePath = "wwwroot" + directory + tims;
                                    if (!Directory.Exists("wwwroot" + directory))
                                    {
                                        DirectoryInfo di = Directory.CreateDirectory(filePath);
                                    }
                                    resizeImage.Save(Path.GetFullPath(filePath), new PngEncoder());
                                    filePath = origindirectory + tims;
                                    break;
                            }
                        }
                    }
                }
                return filePath;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                return string.Empty;
            }
        }
        public static string GetFileExtension(string base64String)
        {
            var data = base64String.Substring(0, 5);

            switch (data.ToUpper())
            {
                case "IVBOR":
                    return "png";
                case "/9J/4":
                    return "jpg";
                case "AAAAF":
                    return "mp4";
                case "JVBER":
                    return "pdf";
                case "AAABA":
                    return "ico";
                case "UMFYI":
                    return "rar";
                case "E1XYD":
                    return "rtf";
                case "U1PKC":
                    return "txt";
                case "MQOWM":
                case "77U/M":
                    return "srt";
                default:
                    return string.Empty;
            }
        }
        public static AttachmentType CheckTypeBase64(string value)
        {

            if (String.IsNullOrEmpty(value))
                return new AttachmentType
                {
                    FriendlyName = "Unknown",
                    MimeType = "application/octet-stream",
                    Extension = ""
                };

            var data = value.Substring(0, 5);
            switch (data.ToUpper())
            {
                case "IVBOR":
                    return new AttachmentType
                    {
                        FriendlyName = "Photo",
                        MimeType = "image/png",
                        Extension = ".png"
                    };
                case "AAAAF":
                    return new AttachmentType
                    {
                        FriendlyName = "Video",
                        MimeType = "video/mp4",
                        Extension = ".mp4"
                    };
                default:
                    return new AttachmentType
                    {
                        FriendlyName = "Unknown",
                        MimeType = string.Empty,
                        Extension = ""
                    };
            }
        }


        public static Tuple<int, int> PrepareSizeImage(Image<Rgba32> image)
        {
            decimal width = 0, height = 0;
            int maxWidth = 2560;
            int maxHeight = 1600;
            var RaW = (decimal)maxWidth / (decimal)image.Width;
            var RaH = (decimal)maxHeight / (decimal)image.Height;
            decimal ratio = Math.Min((decimal)RaW, (decimal)RaH);
            height = image.Height * (decimal)ratio;
            width = image.Width * (decimal)ratio;
            return Tuple.Create((int)width, (int)height);
        }

        public static Tuple<string,string> TicketSaveFile(string fileBase64, string directory, string origindirectory, string fileName)
        {
            try
            {
                string filePath = string.Empty;//data:application/octet-stream;base64,
                string pattern = @"data:.*?base64,";
                fileBase64 = Regex.Replace(fileBase64, pattern, String.Empty);

                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                string extension = Path.GetExtension(fileName);

                string newFileName = fileNameWithoutExtension + $"_{DateTime.Now.ToString("yymmddhhmmssfff")}" + extension;

                string path_wwwroot = DirectoryHelper.PATH_TICKET;


                byte[] bytes = Convert.FromBase64String(fileBase64);
                //string timee = fileName + "_" +DateTime.Now.ToString("yymmddhhmmssfff") + ".ttf";
                filePath = "wwwroot" + directory + newFileName;
                //var path_check = Path.Combine(Directory.GetCurrentDirectory(), path_wwwroot);
                if (!Directory.Exists("wwwroot" + directory))
                {
                    DirectoryInfo di = Directory.CreateDirectory("wwwroot" + directory);
                }
                //var path_save_file = Path.Combine(Directory.GetCurrentDirectory(), path_wwwroot, fileName);
                //Console.WriteLine("Path Save:" + path_save_file);
                File.WriteAllBytes(filePath, bytes);
                filePath = origindirectory + newFileName;

                return Tuple.Create(filePath, newFileName);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                return Tuple.Create(string.Empty, string.Empty);
            }
        }

    }
}
public class AttachmentType
{
    public string MimeType { get; set; }
    public string FriendlyName { get; set; }
    public string Extension { get; set; }
}