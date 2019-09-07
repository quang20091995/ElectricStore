using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricStore.Logic
{
    public interface IProductDetailRepository
    {
        ProductDetail GetById(int id);
        void Insert(ProductDetail product_detail);
        void Update(ProductDetail product_detail);
        void Delete(int id);
    }
}
