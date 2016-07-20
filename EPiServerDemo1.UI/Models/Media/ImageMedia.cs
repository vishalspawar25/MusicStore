using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Framework.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace EPiServerDemo1.UI.Models.Media
{
    [ContentType(DisplayName = "ImageMedia", GUID = "ddb28396-2e16-4050-ae60-ccf475d6a7d8", Description = "")]
    [MediaDescriptor(ExtensionString = "jpg,jpeg,jpe,ico,gif,bmp,png")]
    public class ImageMedia : MediaData
    {

        [CultureSpecific]
        [Editable(true)]
        [Display(
            Name = "Description",
            Description = "Description field's description",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual string Description { get; set; }
    }
    }
     
