using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectricStore.Models.Request
{
    public class AddProductDetailRequest
    {
        public int ProductId { get; set; }
        public string Microprocessor { get; set; }
        public Nullable<double> Speed { get; set; }
        public string Graphics { get; set; }
        public string RAM { get; set; }
        public Nullable<int> Capacity { get; set; }
        public string Hardware { get; set; }
        public string Monitor { get; set; }
        public Nullable<double> Monitorsize { get; set; }
        public string Operation { get; set; }
        public string Color { get; set; }
        public string Connection { get; set; }
        public string Gate { get; set; }
        public Nullable<bool> Webcam { get; set; }
        public Nullable<bool> Recognition { get; set; }
        public string Battery { get; set; }
        public string Size { get; set; }
        public Nullable<double> Weight { get; set; }
        public string Description { get; set; }
        public string Core { get; set; }
        public string Disc { get; set; }
    }
}