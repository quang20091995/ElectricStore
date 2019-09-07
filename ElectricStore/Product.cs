//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ElectricStore
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        public int ProductId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string ProductName { get; set; }
        public Nullable<int> ManufactureId { get; set; }
        public Nullable<decimal> ProductPrice { get; set; }
        public string ProductImage { get; set; }
        public Nullable<bool> StockStatus { get; set; }
        public Nullable<int> AlbumId { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Company Company { get; set; }
        public virtual ProductDetail ProductDetail { get; set; }
    }
}
