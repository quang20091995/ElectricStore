using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElectricStore.Models.Request
{
    public class AddProductRequest
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn loại sản phẩm")]       
        public Nullable<int> CategoryId { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        [MaxLength(300, ErrorMessage = "Tên sản phẩm dài quá kí tự cho phép")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn hãng sản phẩm")]
        public Nullable<int> ManufactureId { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập giá sản phẩm")]
        public Nullable<decimal> ProductPrice { get; set; }
        public string ProductImage { get; set; }
        public Nullable<bool> StockStatus { get; set; }


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
        public Nullable<double> Battery { get; set; }
        public string Size { get; set; }
        public Nullable<double> Weight { get; set; }
        public string Description { get; set; }
        public string Core { get; set; }
        public string Disc { get; set; }    
        

    }
}