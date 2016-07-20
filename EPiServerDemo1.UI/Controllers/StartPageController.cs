using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web.Mvc;
using EPiServerDemo1.UI.Models.Pages;
using EPiServer.ServiceLocation;
using EPiServer.DataAbstraction;
using EPiServerDemo1.UI.Models.ViewModels;
using System;
using EPiServer.Web;
using System.Threading;
using System.Globalization;
using EPiServer.Globalization;

namespace EPiServerDemo1.UI.Controllers
{
    public class StartPageController : PageController<StartPage>
    {
        public ActionResult Index(StartPage currentPage, string id)
        {
            var parentURL = currentPage.URLSegment;



            ProductCatalogViewModel pvm = new ProductCatalogViewModel();
            //SearchContentModel sm = new SearchContentModel();
            //sm.SearchPageUrl = "/en/search";
            //pvm.SearchModel = sm;
            pvm.ParentURL = parentURL;
            if (id == null)
            {
                /* Implementation of action. You can create your own view model class that you pass to the view or
                 * you can pass the page type for simpler templates */

                IContentRepository contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();
                StartPage startPage = contentRepository.Get<StartPage>(new ContentReference(12));
                List<Product> listProduct = new List<Product>();
                foreach (var product in ServiceLocator.Current.GetInstance<IContentRepository>().GetChildren<Product>(startPage.PageReference))
                {

                    listProduct.Add(product);

                }
                pvm.listProduct = listProduct;

                FillCategories(pvm);


            }
           
            // Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("hi-IN");
            GetFriendlyURL(currentPage);
            // ContentLanguage.Instance.SetCulture("hi-IN");
            // UserInterfaceLanguage.Instance.SetCulture("hi-IN");
            string url = ServiceLocator.Current.GetInstance<EPiServer.Web.Routing.UrlResolver>().GetVirtualPath(currentPage.ContentLink).ToString();
            return View(pvm);
        }

        [HttpPost]
        public ActionResult Index(StartPage currentPage, string ddlGenre, FormCollection f)
        {

            var parentURL = currentPage.URLSegment;

            ProductCatalogViewModel pvm = new ProductCatalogViewModel();
            pvm.ParentURL = parentURL;

            List<Product> listProduct = new List<Product>();
           // var d = ServiceLocator.Current.GetInstance<IContentRepository>().GetChildren<Product>(currentPage.ContentLink);
            foreach (var product in ServiceLocator.Current.GetInstance<IContentRepository>().GetChildren<Product>(new ContentReference(12)))
            {
                if (ddlGenre != "0")
                {
                    var lstategories = product.Category.ToString().Split(',');
                    foreach (var item in lstategories)
                    {
                        if (item == ddlGenre)
                            listProduct.Add(product);
                    }
                }
                else
                    listProduct.Add(product);

            }
            pvm.listProduct = listProduct;
            if (Session["categories"] != null)
            { pvm.LstCategories = (List<SelectListItem>)Session["categories"]; }
            else
            { FillCategories(pvm); }
            //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("hi-IN");
            //ContentLanguage.Instance.SetCulture("hi-IN");
            //UserInterfaceLanguage.Instance.SetCulture("hi-IN");
            return View(pvm);

        }

        [NonAction]

        private void FillCategories(ProductCatalogViewModel pvm)
        {
            CategoryRepository categoryRepository = ServiceLocator.Current.GetInstance<CategoryRepository>();

            CategoryCollection categoriesGenre = categoryRepository.Get("Genre").Categories;
            pvm.LstCategories = new List<SelectListItem>();
            pvm.LstCategories.Insert(0, new SelectListItem { Text = "All", Value = "0" });
            foreach (var item in categoriesGenre)
            {
                pvm.LstCategories.Add(new SelectListItem
                {
                    Text = item.Description,
                    Value = item.ID.ToString()
                });
            }
            Session["categories"] = pvm.LstCategories;
        }

        public string GetFriendlyURL(PageData p)
        {
            UrlBuilder pageURLBuilder = new UrlBuilder(p.LinkURL);

            Global.UrlRewriteProvider.ConvertToExternal(pageURLBuilder, p.PageLink, System.Text.UTF8Encoding.UTF8);

            string pageURL = pageURLBuilder.ToString();

            UriBuilder uriBuilder = new UriBuilder(p.URLSegment);

            uriBuilder.Path = pageURL;

            return uriBuilder.Uri.AbsoluteUri;
        }

        
    }
}