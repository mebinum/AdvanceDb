using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace Sitecore.SharedSource.SearchCrawler.DynamicFields
{
    public class ParentNameField : BaseDynamicField
    {
        public override string ResolveValue(Item item)
        {
            Assert.ArgumentNotNull(item, "item");

            Assert.ArgumentNotNull(item, "item");

            if (item.TemplateName == "SLR")
            {
                return string.Join(" ", item.Parent.Name, item.Parent.Parent.Name);
            }

            return string.Empty;
        }
    }
}
