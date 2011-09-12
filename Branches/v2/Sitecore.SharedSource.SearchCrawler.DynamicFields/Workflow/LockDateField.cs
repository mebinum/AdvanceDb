using System;
using Lucene.Net.Documents;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace Sitecore.SharedSource.SearchCrawler.DynamicFields.Workflow
{
   public class LockDateField : BaseDynamicField
   {
      public override string ResolveValue(Item item)
      {
         Assert.ArgumentNotNull(item, "item");

         if (item.Locking.IsLocked())
         {
            LockField lockField = item.Fields[FieldIDs.Lock];

            if (lockField.Date > DateTime.MinValue)
            {
               return DateTools.DateToString(lockField.Date, DateTools.Resolution.DAY);
            }
         }

         return string.Empty;
      }
   }
}
