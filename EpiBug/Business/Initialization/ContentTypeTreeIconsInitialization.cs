using System.Linq;
using System.Reflection;
using EpiBug.Models;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.Shell;

namespace EpiBug.Business.Initialization
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class ContentTypeTreeIconsInitialization : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            SetIcons(context.Locate.Advanced.GetInstance<UIDescriptorRegistry>());
        }
        private static void SetIcons(UIDescriptorRegistry uiDescriptorRegistry)
        {
            var instances = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetCustomAttribute<ContentTypeIconAttribute>() != null);
            var descriptors = uiDescriptorRegistry.UIDescriptors.ToList();

            foreach (var instance in instances)
            {
                var icon = instance.GetCustomAttributes<ContentTypeIconAttribute>().FirstOrDefault()?.CssClass;
                var descriptor = descriptors.FirstOrDefault(x => x.ForType.FullName == instance.ToString());
                if (descriptor != null)
                    descriptor.IconClass = icon;
            }
        }
        public void Preload(string[] parameters)
        {
        }
        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}