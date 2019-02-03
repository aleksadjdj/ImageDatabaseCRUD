using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageDatabaseCRUD.Models
{
    public class ImageViewModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public byte[] ImageData { get; set; }

        public HttpPostedFileBase File { get; set; }

        public string ImageSrc
        {
            get
            {
                string base64 = Convert.ToBase64String(ImageData);
                string imgSrc = String.Format("data:image/png;base64,{0}", base64);
                return imgSrc;
            }

        }
    }
}