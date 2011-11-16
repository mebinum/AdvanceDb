using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace Sitecore.SharedSource.SearchCrawler.DynamicFields.Content
{
    public class DefaultIconField : BaseDynamicField
    {
        public override string ResolveValue(Item item)
        {
            Assert.ArgumentNotNull(item, "item");
            return item.Appearance.Icon.ToLowerInvariant().Contains("document.png") ? "1" : null;
        }
    }
}
