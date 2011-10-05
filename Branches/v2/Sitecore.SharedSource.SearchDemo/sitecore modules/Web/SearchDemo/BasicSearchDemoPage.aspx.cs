using Sitecore.SharedSource.Searcher;
using Sitecore.SharedSource.Searcher.Parameters;

namespace Sitecore.SharedSource.SearchDemo
{
   using System.Collections.Generic;

    public partial class BasicSearchDemoPage : BaseDemoPage
   {
      public override List<SkinnyItem> GetItems(string databaseName,
                                                string indexName,
                                                string language,
                                                string templateFilter,
                                                string locationFilter,
                                                string fullTextQuery)
      {
          var searchParam = new SearchParam
                                {
                                 Database = databaseName,
                                 Language = language,
                                 TemplateIds = templateFilter,
                                 LocationIds = locationFilter,
                                 FullTextQuery = fullTextQuery
                              };

         using (var runner = new QueryRunner(indexName))
         {
            return runner.GetItems(searchParam);
         }
      }
   }
}