using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAccess;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServerDemo1.UI.Models.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Xml;

namespace EPiServerDemo1.UI.modules.MyCustomPlugin
{
    [EPiServer.PlugIn.GuiPlugIn(Area = EPiServer.PlugIn.PlugInArea.AdminConfigMenu,
           Url = "/modules/MyCustomPlugin/MyCustomPlugin/Index", DisplayName = "My Custom Plugin")]
    public class MyCustomPluginController : Controller
    {
        // GET: MyCustomPlugin
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            IContentRepository contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();
            IContentTypeRepository contentTypeRepository = ServiceLocator.Current.GetInstance<IContentTypeRepository>();


            #region Add Products
            StartPage startPage = contentRepository.Get<StartPage>(new ContentReference(12));
            if (startPage != null && startPage.PageReference != null)
            {
                XmlDocument xmlData = new XmlDocument();
                xmlData.Load(file.InputStream);
                XmlNodeList productNodes = xmlData.DocumentElement.SelectNodes("/Products");
                var categoryRepository = ServiceLocator.Current.GetInstance<CategoryRepository>();
                int productCount = 0;
                foreach (XmlNode item in productNodes[0].ChildNodes)
                {
                    Product productPage = contentRepository.GetDefault<Product>(startPage.PageReference);

                    //checking for dupliacte product
                    if (!IsDuplicatePage(item.Attributes["PageName"].InnerText))
                    {
                        productPage.Name = item.Attributes["PageName"].InnerText;
                        productPage.URLSegment = UrlSegment.CreateUrlSegment(productPage);
                        productPage.ProductId = item.Attributes["Id"].InnerText;

                        productPage.SongTitle = item["Name"].InnerText;
                        productPage.Singer = item["Singer"].InnerText;
                        productPage.Description = item["Description"].InnerText;

                        // adding categories to pages
                      
                        foreach (XmlNode category in item.ChildNodes[1].ChildNodes)
                        {
                            //getting category by name  from repository                         
                            Category ct = categoryRepository.Get(category.InnerText);
                            if (ct != null)
                            {
                                //if match found then add its id to product page.                              
                                productPage.Category.Add(ct.ID);
                            }
                        }                   
                        contentRepository.Save(productPage, SaveAction.Publish, EPiServer.Security.AccessLevel.NoAccess);
                        productCount++;
                    }

                }
                ViewBag.message = productCount+ " Product(s) added successfully!";
            }

            #endregion

            return View();
        }

        public ActionResult Delete()
        {
            IContentRepository contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();
            IContentTypeRepository contentTypeRepository = ServiceLocator.Current.GetInstance<IContentTypeRepository>();
            contentRepository.DeleteChildren(new ContentReference(12), true, EPiServer.Security.AccessLevel.NoAccess);
            ViewBag.message = "All Products deleted sucessfully!";
            return View("Index");
        }

        [NonAction]
        private bool IsDuplicatePage(string PageName)
        {
            bool flag = false;

            IContentRepository contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();
            StartPage startPage = contentRepository.Get<StartPage>(new ContentReference(12));
            List<Product> listProduct = new List<Product>();
            foreach (var product in ServiceLocator.Current.GetInstance<IContentRepository>().GetChildren<Product>(startPage.PageReference))
            {
                if (product.Name == PageName)
                {
                    flag = true;

                }

            }
            return flag;
        }
    }
}