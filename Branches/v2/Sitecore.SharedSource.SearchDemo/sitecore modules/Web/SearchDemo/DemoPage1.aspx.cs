using SearchDemo.Scripts;
using Sitecore.SharedSource.Searcher;
using Sitecore.SharedSource.Searcher.Parameters;

namespace Sitecore.SharedSource.SearchDemo
{
   using System.Collections.Generic;

   public partial class DemoPage1 : BaseDemoPage
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
                                 FullTextQuery = fullTextQuery
                              };

         using (var runner = new QueryRunner(indexName))
         {
            return runner.GetItems(searchParam);
         }
      }
   }
}