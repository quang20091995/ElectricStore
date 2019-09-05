using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectricStore.Models.Response
{
    public class ProductElement
    {
        public int ProductId { get; set; }
        public String ProductName { get; set; }
        public String CategoryName { get; set; }
        public String Manufacture { get; set; }
        public decimal? ProductPrice { get; set; }
        public String ProductImage { get; set; }
        public Boolean? StockStatus { get; set; }

    }
}