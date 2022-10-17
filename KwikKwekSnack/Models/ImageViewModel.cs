using KwikKwekSnack.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace KwikKwekSnack.Models
{
    public class ImageViewModel
    {
        public IFormFile ImageFile { get; set; }
        public byte[] ImageData { get; set; } 
        public string ImageDataUrl { get; set; }

        public byte[] ConvertFileToByteArray()
        {
            MemoryStream ms = new MemoryStream();
            ImageFile.CopyTo(ms);
            ImageData = ms.ToArray();
            ms.Close();
            ms.Dispose();
            return ImageData;
        }

        public string ConvertByteArrayToDataUrl(byte[] imgBytes)
        {
            string imageBase64Data = Convert.ToBase64String(imgBytes);
            ImageDataUrl = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
            return ImageDataUrl;
        }
    }

    

}
