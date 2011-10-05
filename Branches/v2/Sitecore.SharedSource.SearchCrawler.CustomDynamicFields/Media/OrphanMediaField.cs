using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace Sitecore.SharedSource.SearchCrawler.DynamicFields.Media
{
   public class OrphanMediaField : BaseDynamicField
   {
      public override string ResolveValue(Item item)
      {
         Assert.ArgumentNotNull(item, "item");

         if (item.Paths.IsMediaItem && !item.TemplateID.Equals(TemplateIDs.MediaFolder))
         {
            var mediaItem = new MediaItem(item);

            return mediaItem.HasMediaStream("{40E50ED9-BA07-4702-992E-A912738D32DC}") ? "0" : "1";
         }

         return null;
      }
   }
}
