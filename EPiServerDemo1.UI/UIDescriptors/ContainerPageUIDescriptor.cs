
using EPiServer.Shell;
using EPiServerDemo1.UI.Models.Pages;

namespace EPiServerSite1.Business.UIDescriptors
{
    /// <summary>
    /// Describes how the UI should appear for <see cref="ContainerPage"/> content.
    /// </summary>
    [UIDescriptorRegistration]
    public class ContainerPageUIDescriptor : UIDescriptor<Product>
    {
        public ContainerPageUIDescriptor()
            : base("folder-icon")
        {
            DefaultView = CmsViewNames.AllPropertiesView;
        }
    }
}
