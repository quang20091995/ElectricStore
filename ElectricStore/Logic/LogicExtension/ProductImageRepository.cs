using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ElectricStore.Logic.LogicExtension
{
    public class ProductImageRepository:IProductImageRepository
    {
        private readonly LaptopStoreEntities context = new LaptopStoreEntities();

        public bool Delete(int id)
        {
            Album album = GetById(id);
            if (album is null)
            {
                return false;
            }
            context.Albums.Remove(album);
            context.SaveChanges();
            return true;
        }

        public List<Album> GetAllImages(int product_id)
        {
            return context.Albums.Where(x => x.ProductId == product_id).ToList();
        }

        public Album GetById(int id)
        {
            return context.Albums.Find(id);
        }

        public void Insert(Album album)
        {
            context.Albums.Add(album);
            context.SaveChanges();
        }

        public int LengthAlbum(int id)
        {
            return context.Albums.Count(x => x.ProductId.Equals(id));
        }

        public void Update(Album album)
        {
            context.Albums.Attach(album);
            context.Entry(album).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}