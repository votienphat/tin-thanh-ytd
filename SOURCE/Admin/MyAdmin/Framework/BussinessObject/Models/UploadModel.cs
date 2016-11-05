using System;
using BussinessObject.Enums;
using BussinessObject.UploadService;

namespace BussinessObject.Models
{
    public class UploadModel
    {
        public TypeImageUploadEnum TypeImageUpload { get; set; }

        public string Path { get; set; } 

        /// <summary>
        /// Dữ liệu kiểu base64 của Image
        /// </summary>
        public byte[] ByteArray { get; set; }
    }
}
