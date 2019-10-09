using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectricStore.Models
{
    public class ProductFullInfoResponse
    {
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public String CategoryName { get; set; }
        public string ManufactureName { get; set; }
        public Nullable<decimal> ProductPrice { get; set; }
        public string ProductImage { get; set; }
        public Nullable<bool> StockStatus { get; set; }


        public string Microprocessor { get; set; }
        public Nullable<double> Speed { get; set; }
        public string Graphics { get; set; }
        public string RAM { get; set; }
        public int? Capacity { get; set; }
        public string Hardware { get; set; }

        public string Monitor { get; set; }
        public Nullable<double> Monitorsize { get; set; }
        public string Operation { get; set; }

        public string Color { get; set; }
        public string Connection { get; set; }
        public string Gate { get; set; }
        public Nullable<bool> Webcam { get; set; }
        public Nullable<bool> Recognition { get; set; }
        public Nullable<double> Battery { get; set; }
        public string Size { get; set; }
        public Nullable<double> Weight { get; set; }
        public string Description { get; set; }
        public string Core { get; set; }
        public string Disc { get; set; }
    }
}