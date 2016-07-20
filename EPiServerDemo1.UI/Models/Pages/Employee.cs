using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;

namespace EPiServerDemo1.UI.Models.Pages
{
    [ContentType(DisplayName = "Employee", GUID = "5fc6c726-bf49-4754-a5e1-2056101b969f", Description = "")]
    public class Employee : PageData
    {
      
        [Display(Name = "Employee Name")]
        public virtual   string EmployeeName { get; set; }
        [Display(Order = 2)]
        public virtual string Email { get; set; }

        [Display(Order = 3)]
        public virtual string Mobile { get; set; }
        [Display(Order = 4)]
        public virtual XhtmlString Skills { get; set; }

        public virtual ContentArea ExperienceContentArea { get; set; }
        public virtual LinkItemCollection LinkItems{ get; set; }

        /*
                [CultureSpecific]
                [Display(
                    Name = "Main body",
                    Description = "The main body will be shown in the main content area of the page, using the XHTML-editor you can insert for example text, images and tables.",
                    GroupName = SystemTabNames.Content,
                    Order = 1)]
                public virtual XhtmlString MainBody { get; set; }
         */
    }
}