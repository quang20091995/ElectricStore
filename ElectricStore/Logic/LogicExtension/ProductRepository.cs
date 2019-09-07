using ElectricStore.Common.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ElectricStore.Common.IRepository.Repository
{
    public class ProductRepository : IProductRepository
    {
        private LaptopStoreEntities context = new LaptopStoreEntities();
        public void Delete(int id)
        {
            Product product = GetById(id);
            context.Products.Remove(product);
            context.SaveChanges();
        }

        public Product GetById(int id)
        {
            return context.Products.Find(id);
        }

        public void Insert(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }

        public void Update(Product product)
        {
            context.Products.Attach(product);
            context.Entry(product).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}