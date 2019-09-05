using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectricStore.Models.Response
{
    public class AdminProductResponse
    {
        public List<ProductElement> ProductList { get; set; }
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}