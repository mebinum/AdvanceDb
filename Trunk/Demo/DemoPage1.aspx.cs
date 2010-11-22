namespace SearchDemo.Scripts
{
   using System.Collections.Generic;
   using Sitecore.SharedSource.Search;
   using Sitecore.SharedSource.Search.Parameters;
   using Sitecore.SharedSource.Search.Utilities;

   public class DemoPage1 : BaseDemoPage
   {
      public override List<SkinnyItem> GetItems(string indexName,
                                                string language,
                                                string templateFilter,
                                                string locationFilter,
                                                string fullTextQuery)
      {
         var searchParam = new SearchParam
                              {
                                 Language = language,
                                 TemplateIds = templateFilter,
                                 LocationIds = locationFilter,
                                 FullTextQuery = fullTextQuery,
                                 ShowAllVersions = false
                              };

         using (var searcher = new Searcher(indexName))
         {
            return searcher.GetItems(searchParam);
         }
      }
   }
}