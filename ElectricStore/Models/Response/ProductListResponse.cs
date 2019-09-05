using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectricStore.Models.Response
{
    public class ProductListResponse
    {
        public List<Product> ProductList { get; set; }
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}