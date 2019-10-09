using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectricStore.Models.Request
{
    public class AlbumRequest
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Alt { get; set; }
        public string ImagePath { get; set; }
        //public HttpPostedFileBase ImagePath { get; set; }
    }
}