using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;

namespace EPiServerDemo1.UI.Models.Pages
{
    [ContentType(DisplayName = "ProductCatalog", GUID = "d96d54e1-58f5-456e-8d0b-c2381d0737c0", Description = "")]
    public class ProductCatalog : PageData
    {
        [BackingType(typeof(PropertyString))]
        [CultureSpecific]
        [Searchable]
        [Required]
        [Editable(true)]
        [Display(Name = "Catalog Title", Description = "", GroupName = SystemTabNames.Content, Order = 10)]
        public virtual string CatalogTitle { get; set; }
    }
}