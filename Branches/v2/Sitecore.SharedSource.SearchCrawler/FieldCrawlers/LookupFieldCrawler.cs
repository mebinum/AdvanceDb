using System;
using Sitecore.Data.Fields;
using Sitecore.Search.Crawlers.FieldCrawlers;

namespace Sitecore.SharedSource.SearchCrawler.FieldCrawlers
{
    /// <summary>
    /// Defines a way to crawle lookup fields.
    /// </summary>
    public class LookupFieldCrawler : FieldCrawlerBase
    {
        public LookupFieldCrawler(Field field) : base(field){ }

        /// <summary>
        /// Returns lookup field value as an item name instead of item ID.
        /// </summary>
        /// <returns></returns>
        public override string GetValue()
        {
           if (FieldTypeManager.GetField(_field) is LookupField)
           {
              var lookupField = new LookupField(_field);
              var targetItem = lookupField.TargetItem;
              return targetItem != null ? targetItem.DisplayName.ToLowerInvariant() : String.Empty;
           }
           return String.Empty;
        }
    }
}