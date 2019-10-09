using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricStore.Logic
{
    public interface IProductRepository
    {
        Product GetById(int id);
        void Insert(Product product);
        void Update(Product product);
        bool Delete(int id);
        bool CheckProductHavingDetail(int id);
    }
}
