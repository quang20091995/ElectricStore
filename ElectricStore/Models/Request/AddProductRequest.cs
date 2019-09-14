using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ElectricStore.Models.Request
{
    public class AddProductRequest
    {
        public int ProductId { get; set; }
        [Range(1, 50, ErrorMessage = "Vui lòng chọn loại sản phẩm")]
        public int CategoryId { get; set; }
        
        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        [MaxLength(300, ErrorMessage = "Tên sản phẩm dài quá kí tự cho phép")]
        public string ProductName { get; set; }

        [Range(1,50, ErrorMessage = "Vui lòng chọn hãng sản phẩm")]
        public int ManufactureId { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập giá sản phẩm")]
        public decimal ProductPrice { get; set; }
        public string ProductImage { get; set; }
        public bool StockStatus { get; set; }  
    }
}