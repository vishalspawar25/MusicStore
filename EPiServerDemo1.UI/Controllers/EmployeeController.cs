using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web.Mvc;

namespace EPiServerDemo1.UI.Controllers
{
    public class EmployeeController : PageController<Models.Pages.Employee>
    {
        public ActionResult Index(Models.Pages.Employee currentPage)
        {
            /* Implementation of action. You can create your own view model class that you pass to the view or
             * you can pass the page type for simpler templates */

            //List<EPiServer.SpecializedProperties.LinkItem> ls = currentPage.LinkItems.ToList();
            //foreach (var item in ls)
            //{
            //    PageReference pageReference = PageReference.ParseUrl(item.Href);
                
            //    PageData p = DataFactory.Instance.GetPage((pageReference));
            //    item.Href = p.URLSegment;
                

            //}




            return View(currentPage);
        }
    }
}