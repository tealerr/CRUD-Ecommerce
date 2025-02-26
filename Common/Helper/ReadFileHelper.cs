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

    public class ImageHelper
    {
        public static string GetBase64ImageUrl(string base64)
        {
            return $"data:image/png;base64,{base64}";
        }

        public static string GetImageUrlFromPath(string filePath)
        {
            string base64 = ReadFileHelper.ConvertImageToBase64(filePath);
            return GetBase64ImageUrl(base64);
        }
    }
}