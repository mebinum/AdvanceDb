using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace Sitecore.SharedSource.SearchCrawler.DynamicFields.Workflow
{
   public class IsLockedField : BaseDynamicField
   {
      public override string ResolveValue(Item item)
      {
         Assert.ArgumentNotNull(item, "item");
         return item.Locking.IsLocked().ToString().ToLowerInvariant();
      }
   }
}
