using System.Text;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Links;
using Sitecore.SharedSource.Searcher.Utilities;

namespace Sitecore.SharedSource.SearchCrawler.DynamicFields.Content
{
   public class LinksField : BaseDynamicField
   {
      public override string ResolveValue(Item item)
      {
         Assert.ArgumentNotNull(item, "item");
         return GetItemLinks(item);
      }

      protected virtual string GetItemLinks(Item item)
      {
         Assert.ArgumentNotNull(item, "item");
         var builder = new StringBuilder();
         foreach (ItemLink link in item.Links.GetAllLinks(false))
         {
            builder.Append(" ");
            builder.Append(IdHelper.NormalizeGuid(link.TargetItemID));
         }

         return builder.ToString();
      }
   }
}
