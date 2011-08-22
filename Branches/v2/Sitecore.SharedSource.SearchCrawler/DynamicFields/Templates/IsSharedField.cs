using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace Sitecore.SharedSource.SearchCrawler.DynamicFields.Templates
{
   public class IsSharedField : BaseDynamicField
   {
      public override string ResolveValue(Item item)
      {
         Assert.ArgumentNotNull(item, "item");

         if (item.TemplateID.Equals(TemplateIDs.TemplateField))
         {
            return item[TemplateFieldIDs.Shared];
         }

         return null;
      }
   }
}
