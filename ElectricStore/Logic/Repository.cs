using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectricStore.Logic
{
    public interface Repository<T> where T: LaptopStoreEntities
    {
        List<T> GetAll();
        void Add();
        T GetById(int key);
        void Remove(int key);
        void RemoveRange(List<int> listId);
        T Update(int key);
    }
}