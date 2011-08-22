using System.Linq;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.SharedSource.Searcher.Utilities;

namespace Sitecore.SharedSource.SearchCrawler.DynamicFields
{
    public class ContentReferencesField : BaseDynamicField
    {
        public override string ResolveValue(Item item)
        {
            Assert.ArgumentNotNull(item, "item");

            var references = from reference in Globals.LinkDatabase.GetReferences(item)
                             select reference;

            var allReferrers = references.Count();

            if (allReferrers <= 0) return SearchHelper.FormatNumber(0);

            var validReferrers = 0;

            foreach (var reference in references)
            {
                var target = reference.GetTargetItem();

                if (target != null && target.Paths.IsContentItem && !target.ID.Equals(item.ID))
                {
                    validReferrers++;
                }
            }

            return SearchHelper.FormatNumber(validReferrers);
        }
    }
}
