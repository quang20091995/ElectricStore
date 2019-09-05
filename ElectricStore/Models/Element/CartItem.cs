using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ElectricStore.Models.Element
{
    public class CartItem
    {
        public string ProductName { get; set; }
        public decimal? ProductPrice { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }

    }
}