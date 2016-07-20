using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;

namespace EPiServerDemo1.UI.Models.Pages
{
    [ContentType(DisplayName = "SearchPage", GUID = "7ae94ab0-a869-415b-9574-31fc9ac5fbd3", Description = "")]
    public class SearchPage : PageData
    {
        
                [CultureSpecific]
                [Display(
                    Name = "Max Results Per Page",
                    Description = "MaxResultsPerPage",
                    GroupName = SystemTabNames.Content,
                    Order = 1)]
                public virtual int MaxResultsPerPage { get; set; }
         
    }
}