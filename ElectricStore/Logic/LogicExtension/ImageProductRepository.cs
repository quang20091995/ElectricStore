using ElectricStore.Logic;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricStore.Common.IRepository.Repository
{
    public class ImageProductRepository : IImageProductRepository
    {
        private LaptopStoreEntities context = new LaptopStoreEntities();
        public void Delete(int id)
        {
            Album album = context.Albums.Find(id);
            context.Albums.Remove(album);
            context.SaveChanges();
        }

        public IEnumerable<Album> GetAll(int id)
        {
            return context.Albums.Where(x => x.AlbumId.Equals(id)).AsEnumerable();
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

        public void Update(Album album)
        {
            context.Albums.Attach(album);
            context.Entry(album).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
