using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace EPiServerDemo1.UI.Models.Pages
{
    [ContentType(
       DisplayName = "Product",
       GUID = "094688A8-B460-4E41-9077-B8B2924616CF",
       Description = "")]
    public class Product :PageData
    {
        [BackingType(typeof(PropertyString))]
        [Searchable]
        [Required]
        [Display(Name = "Product Id", Description = "", GroupName = SystemTabNames.Content, Order = 10)]
        public virtual string ProductId { get; set; }

        [BackingType(typeof(PropertyString))]
        [CultureSpecific]
        [Searchable]
        [Required]
        [Editable(true)]
        [Display(Name = "Title", Description = "", GroupName = SystemTabNames.Content, Order = 10)]
        public virtual string SongTitle { get; set; }

        [BackingType(typeof(PropertyString))]
        [Required]
        [CultureSpecific]
        [Editable(true)]
        [Display(Name = "Singer", Description = "", GroupName = SystemTabNames.Content, Order = 20)]
        public virtual string Singer { get; set; }

        //[UIHint(UIHint.MediaFile)]
        //[Display(Name = "Product image")]
        //public virtual ContentReference ProductImage { get; set; }
        [CultureSpecific]
        [UIHint(UIHint.Textarea)]
        [Display(Name = "Song Description")]
        [Editable(true)]
        [Required]
        public virtual string Description { get; set; }

      
        //[Display(Name = "Related product block area", Description = "", GroupName = SystemTabNames.Content, Order = 30)]
        //public virtual ContentArea RelatedProductContentArea { get; set; }
    }
}