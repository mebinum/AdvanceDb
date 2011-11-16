using System.Linq;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.SharedSource.Searcher.Utilities;

namespace Sitecore.SharedSource.SearchCrawler.DynamicFields.Content
{
    public class ContentReferencesField : BaseDynamicField
    {
        public override string ResolveValue(Item item)
        {
            Assert.ArgumentNotNull(item, "item");

            var referrers = Globals.LinkDatabase.GetReferrers(item).Select(r => r.GetSourceItem());

            var total = 0;

            if (referrers.Count() > 0) total = referrers.Count(ValidReferrer);

            return SearchHelper.FormatNumber(total);
        }

        protected bool ValidReferrer(Item item)
        {
            return item != null && item.Paths.IsContentItem;
        }
    }
}
