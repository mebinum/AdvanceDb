using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace Sitecore.SharedSource.SearchCrawler.DynamicFields.Media
{
   public class SharedMediaField : BaseDynamicField
   {
      public override string ResolveValue(Item item)
      {
         Assert.ArgumentNotNull(item, "item");

         if (item.Paths.IsMediaItem && !item.TemplateID.Equals(TemplateIDs.MediaFolder))
         {
            // reading blog field
            var field = item.Fields["Blob"];

            if (field == null) return null;

            return field.Shared ? "1" : "0";
         }

         return null;
      }
   }
}
