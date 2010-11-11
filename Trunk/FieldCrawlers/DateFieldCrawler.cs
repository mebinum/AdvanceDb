using System;
using Sitecore.Data.Fields;
using Sitecore.Search.Crawlers.FieldCrawlers;
using Sitecore.SharedSource.Search.Constants;

namespace Sitecore.SharedSource.Search.FieldCrawlers
{
   /// <summary>
   /// Defines a way to crawl date and datetime fields.
   /// </summary>
   public class DateFieldCrawler : FieldCrawlerBase
   {
      public DateFieldCrawler(Field field) : base(field) { }

      /// <summary>
      /// Returns date/datetime field value in yyyyMMddHHmmss format.
      /// </summary>
      /// <returns></returns>
      public override string GetValue()
      {
         if(String.IsNullOrEmpty(_field.Value))
         {
            return String.Empty;
         }

         var dateField = new DateField(_field);
         return dateField.DateTime.ToString(IndexConstants.DateTimeFormat);
      }
   }
}
