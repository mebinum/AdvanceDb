using Sitecore.SharedSource.Searcher;
using Sitecore.SharedSource.Searcher.Parameters;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Sitecore.Search;
using Sitecore.StringExtensions;

namespace Sitecore.SharedSource.SearchDemo
{
   public partial class DemoPage4 : DemoPage3
   {
      protected List<NumericRangeSearchParam.NumericRangeField> NumericRanges
      {
         get
         {
            var ranges = new List<NumericRangeSearchParam.NumericRangeField>();

            if (!NumericFieldName1TextBox.Text.IsNullOrEmpty() && !NumericStart1TextBox.Text.IsNullOrEmpty() && !NumericEnd1TextBox.Text.IsNullOrEmpty())
               ranges.Add(new NumericRangeSearchParam.NumericRangeField(NumericFieldName1TextBox.Text, int.Parse(NumericStart1TextBox.Text), int.Parse(NumericEnd1TextBox.Text)));

            if (!NumericFieldName2TextBox.Text.IsNullOrEmpty() && !NumericStart2TextBox.Text.IsNullOrEmpty() && !NumericEnd2TextBox.Text.IsNullOrEmpty())
               ranges.Add(new NumericRangeSearchParam.NumericRangeField(NumericFieldName2TextBox.Text, int.Parse(NumericStart2TextBox.Text), int.Parse(NumericEnd2TextBox.Text)));

            return ranges;
         }
      }

      public override List<SkinnyItem> GetItems(string indexName,
                                                string language,
                                                string templateFilter,
                                                string locationFilter,
                                                string fullTextQuery)
      {
         var searchParam = new NumericRangeSearchParam
                              {
                                 Ranges = NumericRanges,
                                 LocationIds = locationFilter,
                                 TemplateIds = templateFilter,
                                 FullTextQuery = fullTextQuery,
                                 InnerCondition = GetCondition(InnerNumericRangeOccuranceList),
                                 Language = language
                              };

         using (var runner = new QueryRunner(indexName))
         {
            return runner.GetItems(searchParam);
         }
      }
   }
}