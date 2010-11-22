using System.Collections.Generic;
using Sitecore.SharedSource.Search.Utilities;

namespace SearchDemo.Scripts
{
   public abstract class BaseDemoPage : System.Web.UI.Page
   {
      public abstract List<SkinnyItem> GetItems(string indexName, string language, string templateFilter,
                                                string locationFilter, string fullTextQuery);
   }
}