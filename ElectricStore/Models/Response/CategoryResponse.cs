using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectricStore.Models
{
    public class CategoryResponse
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }


        public CategoryResponse(){}

        public CategoryResponse(int categoryid, string categoryname)
        {
            this.CategoryId = categoryid;
            this.CategoryName = categoryname;
        }
    }
}