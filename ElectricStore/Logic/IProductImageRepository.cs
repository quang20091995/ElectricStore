using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricStore.Logic
{
    public interface IProductImageRepository
    {
        List<Album> GetAllImages(int product_id);
        Album GetById(int id);
        void Insert(Album album);
        void Update(Album album);
        bool Delete(int id);
        int LengthAlbum(int id);
    }
}
