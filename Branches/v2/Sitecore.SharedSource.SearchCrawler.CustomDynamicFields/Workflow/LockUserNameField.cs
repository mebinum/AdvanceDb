using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace Sitecore.SharedSource.SearchCrawler.DynamicFields.Workflow
{
   public class LockUserNameField : BaseDynamicField
   {
      public override string ResolveValue(Item item)
      {
         Assert.ArgumentNotNull(item, "item");

         if (item.Locking.IsLocked())
         {
            LockField lockField = item.Fields[FieldIDs.Lock];
            return lockField.Owner.ToLowerInvariant();
         }

         return string.Empty;
      }

   }
}
