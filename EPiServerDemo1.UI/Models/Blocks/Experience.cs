using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace EPiServerDemo1.UI.Models.Blocks
{
    [ContentType(DisplayName = "Experience", GUID = "fc3bcee2-0388-43b9-9355-f62c055e4f73", Description = "")]
    public class Experience : BlockData
    {
        public virtual string EmployerName { get; set; }
        public virtual string WorkDuration { get; set; }
        public virtual string Designation { get; set; }
       
    }
}