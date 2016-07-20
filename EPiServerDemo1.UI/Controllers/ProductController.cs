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
using System;

namespace EPiServerDemo1.UI.Controllers
{
    public class ProductController : PageController<Product>
    {
        public ActionResult Index(Product currentPage)
        {
            /* Implementation of action. You can create your own view model class that you pass to the view or
             * you can pass the page type for simpler templates */
            // PageDataCollection products = GetChildren(currentPage.ContentLink);

         
            return View(currentPage);
        }


        public PageDataCollection GetChildren(PageReference pageLink)
        {
            return DataFactory.Instance.GetChildren(pageLink);
        }
    }
}