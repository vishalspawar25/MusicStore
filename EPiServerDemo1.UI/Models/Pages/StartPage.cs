using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;

namespace EPiServerDemo1.UI.Models.Pages
{
    [ContentType(DisplayName = "StartPage", GUID = "b0e8eb55-3eb6-4d3b-bc70-1264be44a9fe", Description = "")]
    public class StartPage : PageData
    {
        /*
                [CultureSpecific]
                [Display(
                    Name = "Main body",
                    Description = "The main body will be shown in the main content area of the page, using the XHTML-editor you can insert for example text, images and tables.",
                    GroupName = SystemTabNames.Content,
                    Order = 1)]
                public virtual XhtmlString MainBody { get; set; }
         */
        [CultureSpecific]
        [Searchable]
        [Required]
        public virtual PageReference PageReference { get; set; }
        [CultureSpecific]
        [Searchable]
        
        public virtual ContentArea ProductBlog { get; set; }
        [CultureSpecific]
        [Searchable]
        [Required]
        public virtual PageReference SearchPageReference { get; set; }

        [CultureSpecific]
        [Searchable]
        [Required]
        public virtual PageReference HeaderPageReference { get; set; }
    }
}