using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ElectricStore.Logic.LogicExtension
{
    public class ProductDetailRepository : IProductDetailRepository
    {
        private LaptopStoreEntities context = new LaptopStoreEntities();
        public void Delete(int id)
        {
            ProductDetail product_detail = context.ProductDetails.Find(id);
            context.ProductDetails.Remove(product_detail);
            context.SaveChanges();
        }

        public ProductDetail GetById(int id)
        {
            return context.ProductDetails.Find(id);
        }

        public void Insert(ProductDetail product_detail)
        {
            context.ProductDetails.Add(product_detail);
            context.SaveChanges();
        }

        public void Update(ProductDetail product_detail)
        {
            context.ProductDetails.Attach(product_detail);
            context.Entry(product_detail).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}