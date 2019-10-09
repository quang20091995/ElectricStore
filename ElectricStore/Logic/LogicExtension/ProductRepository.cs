using ElectricStore.Logic;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ElectricStore.Logic.LogicExtension
{
    public class ProductRepository : IProductRepository
    {
        private LaptopStoreEntities context = new LaptopStoreEntities();

        public bool CheckProductHavingDetail(int id)
        {
            var check = true;
            var result = context.ProductDetails.First(x=>x.ProductId.Equals(id));
            if(result.Microprocessor == "" && result.Speed == 0 && result.Graphics == "" 
                && result.RAM == "" && result.Capacity == 0 && result.Hardware == "" && result.Monitor == "" && result.Monitorsize == 0 
                && result.Operation == "" && result.Color == "" && result.Connection == "" && result.Gate == "" && result.Battery == 0 
                && result.Size == "" && result.Weight == 0 && result.Core == "" && result.Disc == "")
            {
                check = false;
            }

            return check;
        }

        public bool Delete(int id)
        {
            Product product = GetById(id);
            if(product is null)
            {
                return false;
            }
            context.Products.Remove(product);
            context.SaveChanges();
            return true;
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