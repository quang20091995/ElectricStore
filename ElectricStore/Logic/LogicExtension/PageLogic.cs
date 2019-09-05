using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectricStore.Logic.LogicExtension
{
    public class PageLogic<T> : IPageLogic<T>
    {
        public List<T> paginate(List<T> data, int current_page, int page_size)
        {
            List<T> lst = data.Skip((current_page - 1) * page_size).Take(page_size).ToList();
            return data.Skip((current_page - 1) * page_size).Take(page_size).ToList();
        }
    }
}