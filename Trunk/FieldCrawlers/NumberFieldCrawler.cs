using System;
using Sitecore.Data.Fields;
using Sitecore.Search.Crawlers.FieldCrawlers;
using Sitecore.SharedSource.Search.Utilities;

namespace Sitecore.SharedSource.Search.FieldCrawlers
{
   public class NumberFieldCrawler : FieldCrawlerBase
   {
      public NumberFieldCrawler(Field field) : base(field) { }

      public override string GetValue()
      {
         int value;

         if (!String.IsNullOrEmpty(_field.Value) && int.TryParse(_field.Value, out value))
         {
            return SearchHelper.FormatNumber(value);
         }

         return String.Empty;
      }
   }
}
