using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace Sitecore.SharedSource.SearchCrawler.DynamicFields
{
   public class HasLayoutField : BaseDynamicField
   {
      public override string ResolveValue(Item item)
      {
         Assert.ArgumentNotNull(item, "item");
         return HasLayout(item) ? "1" : "0";
      }

      protected virtual bool HasLayout(Item item)
      {
         Assert.ArgumentNotNull(item, "item");

         LayoutField layoutField = item.Fields[FieldIDs.LayoutField];

         if (layoutField != null)
         {
            var isStandardValue = layoutField.InnerField.ContainsStandardValue;
            var isEmpty = !layoutField.InnerField.HasValue;

            return !isStandardValue && !isEmpty;
         }

         return false;
      }
   }
}
