using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.SharedSource.Searcher.Utilities;

namespace Sitecore.SharedSource.SearchCrawler.DynamicFields.Content
{
    public class LinkNumberField : BaseDynamicField
    {
        public override string ResolveValue(Item item)
        {
            Assert.ArgumentNotNull(item, "item");

            Assert.ArgumentNotNull(item, "item");

            var allReferrers = Globals.LinkDatabase.GetReferenceCount(item);
            return SearchHelper.FormatNumber(allReferrers);
        }
    }
}
