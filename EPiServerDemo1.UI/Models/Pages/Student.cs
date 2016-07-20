using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;

namespace EPiServerDemo1.UI.Models.Pages
{
    [ContentType(DisplayName = "Student", GUID = "ea3850e0-7189-4a68-ad81-c6800d8a095f", Description = "")]
    public class Student : PageData
    {
        
                [CultureSpecific]
                [Display(
                    Name = "Student Name",
                    Description = "The main body will be shown in the main content area of the page, using the XHTML-editor you can insert for example text, images and tables.",
                    GroupName = SystemTabNames.Content,
                    Order = 1)]
                public virtual string  StudentName { get; set; }
         
    }
}