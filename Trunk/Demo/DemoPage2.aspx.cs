using System.Web.UI.WebControls;

namespace SearchDemo.Scripts
{
   using System.Collections.Generic;
   using Sitecore.SharedSource.Search;
   using Sitecore.SharedSource.Search.Parameters;
   using Sitecore.SharedSource.Search.Utilities;

   public class DemoPage2 : BaseDemoPage
   {
      protected TextBox RelatedIdsTextBox;

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

         using (var searcher = new Searcher(indexName))
         {
            return searcher.GetItems(searchParam);
         }
      }
   }
}