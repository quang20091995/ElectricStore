using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectricStore.Logic
{
    public interface IPageLogic<T>
    {
        List<T> paginate(List<T> data, int current_page, int page_size);
    }
}