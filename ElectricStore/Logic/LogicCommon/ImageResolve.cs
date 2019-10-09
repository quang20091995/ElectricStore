using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ElectricStore.Logic.LogicCommon
{
    public class ImageResolve : IImageResolve
    {
        public string SaveImage(HttpPostedFileBase image)
        {
            var image_path = Path.GetFileName(image.FileName);
            var path = Path.Combine("~/Content/Images/LaptopProduct", image_path);
            image.SaveAs(path);
            return image_path;
        }
    }
}