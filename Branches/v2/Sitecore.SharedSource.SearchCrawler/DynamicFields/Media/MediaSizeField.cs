using System;
using Sitecore.Configuration;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.SharedSource.Searcher.Utilities;

namespace Sitecore.SharedSource.SearchCrawler.DynamicFields.Media
{
   public class MediaSizeField : BaseDynamicField
   {
      public override string ResolveValue(Item item)
      {
         Assert.ArgumentNotNull(item, "item");

         if (item.Paths.IsMediaItem && !item.TemplateID.Equals(TemplateIDs.MediaFolder))
         {
            var mediaItem = new MediaItem(item);

            var size = Math.Round(mediaItem.Size / 1024f);

            return SearchHelper.FormatNumber(size);
         }

         return null;
      }
   }
}
