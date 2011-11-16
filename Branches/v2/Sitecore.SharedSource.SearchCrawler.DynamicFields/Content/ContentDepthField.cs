using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.SharedSource.Searcher.Utilities;

namespace Sitecore.SharedSource.SearchCrawler.DynamicFields.Content
{
    public class ContentDepthField : BaseDynamicField
    {
        public override string ResolveValue(Item item)
        {
            Assert.ArgumentNotNull(item, "item");
            return SearchHelper.FormatNumber(item.Axes.Level);
        }
    }
}
