using Syncfusion.Blazor;

namespace Studentica.UI
{
    public class SyncfusionLocalizer : ISyncfusionStringLocalizer
    {
        public string GetText(string key)
        {
            return ResourceManager.GetString(key);
        }

        public System.Resources.ResourceManager ResourceManager
        {
            get
            {
                return Resources.SfResources.ResourceManager;


            }
        }
    }
}
