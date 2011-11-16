using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.SharedSource.Searcher.Utilities;

namespace Sitecore.SharedSource.SearchCrawler.DynamicFields.Content
{
   public class VersionNumberField : BaseDynamicField
   {
      public override string ResolveValue(Item item)
      {
         Assert.ArgumentNotNull(item, "item");

         return SearchHelper.FormatNumber(item.Versions.GetVersions(false).Length);
      }
   }
}
