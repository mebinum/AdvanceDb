using System.Web.UI.WebControls;
using SearchDemo.Scripts;
using Sitecore.SharedSource.Searcher;
using Sitecore.SharedSource.Searcher.Parameters;
using System.Collections.Generic;

namespace Sitecore.SharedSource.SearchDemo
{
    public partial class DemoPage2 : BaseDemoPage
   {

      protected string RelationFilter
      {
         get { return RelatedIdsTextBox.Text; }
      }

      public override List<SkinnyItem> GetItems(string indexName,
                                                string language,
                                                string templateFilter,
                                                string locationFilter,
                                                string fullTextQuery)
            
      {
         var searchParam = new SearchParam { Language = language,
                                             RelatedIds = RelationFilter,
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