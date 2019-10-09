using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectricStore.Logic
{
    public interface IImageResolve
    {
        string SaveImage(HttpPostedFileBase image);
    }
}