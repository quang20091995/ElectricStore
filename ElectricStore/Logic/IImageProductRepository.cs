using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricStore.Logic
{
    public interface IImageProductRepository
    {
        IEnumerable<Album> GetAll(int id);
        Album GetById(int id);
        void Insert(Album album);
        void Update(Album album);
        void Delete(int id);
    }
}
