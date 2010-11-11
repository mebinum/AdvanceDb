using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.SharedSource.Search.Constants;

namespace Sitecore.SharedSource.Search.DynamicFields
{
   public class LockDateField : BaseDynamicField
   {
      public override string ResolveValue(Item item)
      {
         Assert.ArgumentNotNull(item, "item");

         if (item.Locking.IsLocked())
         {
            LockField lockField = item.Fields[FieldIDs.Lock];
            return lockField.Date.ToString(IndexConstants.DateTimeFormat);
         }

         return string.Empty;
      }
   }
}
