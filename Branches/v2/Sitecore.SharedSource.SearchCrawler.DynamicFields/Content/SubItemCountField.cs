using Sitecore.Collections;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.SharedSource.Searcher.Utilities;

namespace Sitecore.SharedSource.SearchCrawler.DynamicFields
{
   public class SubItemCountField : BaseDynamicField
   {
      public override string ResolveValue(Item item)
      {
         Assert.ArgumentNotNull(item, "item");
         return SearchHelper.FormatNumber(item.GetChildren(ChildListOptions.IgnoreSecurity).Count);
      }
   }
}
