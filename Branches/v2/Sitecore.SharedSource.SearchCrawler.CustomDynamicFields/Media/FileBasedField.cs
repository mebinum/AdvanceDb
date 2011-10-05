using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace Sitecore.SharedSource.SearchCrawler.DynamicFields.Media
{
   public class FileBasedField : BaseDynamicField
   {
      public override string ResolveValue(Item item)
      {
         Assert.ArgumentNotNull(item, "item");

         if (item.Paths.IsMediaItem && !item.TemplateID.Equals(TemplateIDs.MediaFolder))
         {
            var mediaItem = new MediaItem(item);

            return mediaItem.FileBased ? "1" : "0";
         }

         return null;
      }
   }
}
