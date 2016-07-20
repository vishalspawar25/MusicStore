using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServerDemo1.UI.Models.Pages;
using System.Web.Mvc;

namespace EPiServerDemo1.UI.Models.ViewModels
{
    public class ProductCatalogViewModel
    {
        public virtual List<Product> listProduct { set; get; }
        public virtual List<SelectListItem> LstCategories { set; get; }
        public virtual string ParentURL { get; set; }
        public virtual SearchContentModel SearchModel { get; set; }
    }
}