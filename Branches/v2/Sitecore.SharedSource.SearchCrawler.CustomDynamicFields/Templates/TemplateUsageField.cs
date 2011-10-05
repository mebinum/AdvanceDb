using System;
using System.Linq;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.SharedSource.Searcher.Utilities;

namespace Sitecore.SharedSource.SearchCrawler.DynamicFields.Templates
{
   public class TemplateUsageField : BaseDynamicField
   {
      public override string ResolveValue(Item item)
      {
         Assert.ArgumentNotNull(item, "item");

         if (item.Database.Engines.TemplateEngine.IsTemplate(item))
         {
            var referrers = from itm in Globals.LinkDatabase.GetReferrers(item)
                            select item;
            
            var allReferrers = referrers.Count();

            if (allReferrers <= 0) return SearchHelper.FormatNumber(0);

            var validReferrers = 0;

            foreach (var referrer in referrers)
            {
               if (referrer != null && !referrer.Name.Equals("__Standard Values", StringComparison.OrdinalIgnoreCase))
               {
                  validReferrers++;
               }
            }

            return SearchHelper.FormatNumber(validReferrers);
         }

         return null;
      }
   }
}
