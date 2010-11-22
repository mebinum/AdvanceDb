namespace SearchDemo.Scripts
{
   using System;
   using System.Collections.Generic;
   using System.Web.UI.WebControls;
   using Sitecore.Search;
   using Sitecore.SharedSource.Search;
   using Sitecore.SharedSource.Search.Parameters;
   using Sitecore.SharedSource.Search.Utilities;
   using Sitecore.StringExtensions;

   public class DemoPage4 : DemoPage2
   {
      #region Fields

      protected TextBox FieldName1TextBox;
      protected TextBox Start1TextBox;
      protected TextBox End1TextBox;
      protected TextBox FieldName2TextBox;
      protected TextBox Start2TextBox;
      protected TextBox End2TextBox;

      #endregion

      protected List<NumericRangeSearchParam.NumericRangeField> Ranges
      {
         get
         {
            var ranges = new List<NumericRangeSearchParam.NumericRangeField>();

            if (!FieldName1TextBox.Text.IsNullOrEmpty() && !Start1TextBox.Text.IsNullOrEmpty() && !End1TextBox.Text.IsNullOrEmpty())
               ranges.Add(new NumericRangeSearchParam.NumericRangeField(FieldName1TextBox.Text, int.Parse(Start1TextBox.Text), int.Parse(End1TextBox.Text)));

            if (!FieldName2TextBox.Text.IsNullOrEmpty() && !Start2TextBox.Text.IsNullOrEmpty() && !End2TextBox.Text.IsNullOrEmpty())
               ranges.Add(new NumericRangeSearchParam.NumericRangeField(FieldName2TextBox.Text, int.Parse(Start2TextBox.Text), int.Parse(End2TextBox.Text)));

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
                                 Ranges = Ranges,
                                 LocationIds = locationFilter,
                                 TemplateIds = templateFilter,
                                 FullTextQuery = fullTextQuery,
                                 Occurance = QueryOccurance.Must,
                                 Language = language,
                                 ShowAllVersions = false
                              };

         using (var searcher = new Searcher(indexName))
         {
            return searcher.GetItemsInRange(searchParam);
         }
      }
   }
}