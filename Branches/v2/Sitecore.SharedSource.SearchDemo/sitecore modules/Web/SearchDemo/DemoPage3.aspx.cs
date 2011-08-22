using System.Web.UI.WebControls;
using Sitecore.SharedSource.Searcher;
using Sitecore.SharedSource.Searcher.Parameters;
using System.Collections.Generic;
using Sitecore.Search;
using Sitecore.StringExtensions;

namespace Sitecore.SharedSource.SearchDemo
{
   public partial class DemoPage3 : DemoPage2
   {
      protected List<MultiFieldSearchParam.Refinement> GetRefinements()
      {
         var refinements = new List<MultiFieldSearchParam.Refinement>();

         if (!FieldName1TextBox.Text.IsNullOrEmpty() && !FieldValue1TextBox.Text.IsNullOrEmpty())
            refinements.Add(new MultiFieldSearchParam.Refinement(FieldName1TextBox.Text, FieldValue1TextBox.Text));

         if (!FieldName2TextBox.Text.IsNullOrEmpty() && !FieldValue2TextBox.Text.IsNullOrEmpty())
            refinements.Add(new MultiFieldSearchParam.Refinement(FieldName2TextBox.Text, FieldValue2TextBox.Text));

         return refinements;
      }

      public override List<SkinnyItem> GetItems(string indexName,
                                                string language,
                                                string templateFilter,
                                                string locationFilter,
                                                string fullTextQuery)
      {
         var refinements = GetRefinements();

         var searchParam = new MultiFieldSearchParam
                              {
                                 Refinements = refinements,
                                 InnerCondition = GetCondition(OccuranceList),
                                 LocationIds = locationFilter,
                                 TemplateIds = templateFilter,
                                 FullTextQuery = fullTextQuery,
                                 Language = language
                              };

         using (var runner = new QueryRunner(indexName))
         {
            return runner.GetItems(searchParam);
         }
      }
   }
}