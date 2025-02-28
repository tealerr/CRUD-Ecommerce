using Microsoft.AspNetCore.Http;
using SharpCompress.Common;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Common.Helper
{
    public class ReadFileHelper
    {
        public static string ConvertImageToBase64(string filePath)
        {
            byte[] imageBytes = File.ReadAllBytes(filePath);
            return Convert.ToBase64String(imageBytes);
        }

        public static bool IsValidBase64(string base64String)
        {
            if (string.IsNullOrEmpty(base64String))
            {
                return false;
            }

            // แก้ไข padding ให้ครบถ้วน
            int paddingCount = base64String.Length % 4;
            if (paddingCount > 0)
            {
                base64String = base64String.PadRight(base64String.Length + (4 - paddingCount), '=');
            }

            // ตรวจสอบความยาวหลังจากเพิ่ม padding
            if (base64String.Length % 4 != 0)
            {
                return false; // ความยาวไม่สามารถหารด้วย 4 ได้
            }

            try
            {
                // พยายามแปลง Base64 ถ้าไม่สำเร็จจะทำให้เกิดข้อผิดพลาด
                Convert.FromBase64String(base64String);
                return true; // แปลงได้สำเร็จ
            }
            catch
            {
                return false; // หากแปลงไม่ได้ให้ return false
            }
        }
    }

    public partial class ImageHelper
    {
        public static string GetBase64ImageUrl(string base64)
        {
            return $"data:image/png;base64,{base64}";
        }

        public static string SaveImage(string imageBase64, string directory, FileType fileType)
        {
            try
            {
                string filePath = string.Empty;

                imageBase64 = MyRegex1().Replace(imageBase64, string.Empty);
                byte[] bytes = Convert.FromBase64String(imageBase64);

                if (!string.IsNullOrEmpty(imageBase64))
                {
                    using Image<Rgba32> resizeImage = Image.Load<Rgba32>(bytes);
                    {
                        var targetSize = PrepareSizeImage(resizeImage);
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
                                break;

                            case FileType.Png:
                                string tims = DateTime.Now.ToString("yymmddhhmmssfff") + ".png";
                                filePath = "wwwroot" + directory + tims;
                                if (!Directory.Exists("wwwroot" + directory))
                                {
                                    DirectoryInfo di = Directory.CreateDirectory(filePath);
                                }
                                resizeImage.Save(Path.GetFullPath(filePath), new JpegEncoder() { Quality = 75 });
                                break;
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

        public static string GetImageUrl(string imageUrl, HttpContext httpContext)
        {
            var absoluteUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/{imageUrl}";
            return absoluteUrl;
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

        public enum FileType
        {
            JPeg,
            Png,
        }

        [GeneratedRegex(@"^data:image\/[a-zA-Z]+;base64,")]
        private static partial Regex MyRegex();
        [GeneratedRegex(@"^data:image\/[a-zA-Z]+;base64,")]
        private static partial Regex MyRegex1();
    }

}