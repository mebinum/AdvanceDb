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

   public class DemoPage5 : DemoPage2
   {
      #region Fields

      protected TextBox FieldName1TextBox;
      protected TextBox StartDate1TextBox;
      protected TextBox EndDate1TextBox;
      protected TextBox FieldName2TextBox;
      protected TextBox StartDate2TextBox;
      protected TextBox EndDate2TextBox;

      #endregion

      protected List<DateRangeSearchParam.DateRangeField> Ranges
      {
         get
         {
            var dateRanges = new List<DateRangeSearchParam.DateRangeField>();

            if (!FieldName1TextBox.Text.IsNullOrEmpty() && !StartDate1TextBox.Text.IsNullOrEmpty() &&
                !EndDate1TextBox.Text.IsNullOrEmpty())
               dateRanges.Add(new DateRangeSearchParam.DateRangeField(FieldName1TextBox.Text,
                                                                      DateTime.Parse(StartDate1TextBox.Text),
                                                                      DateTime.Parse(EndDate1TextBox.Text)));

            if (!FieldName2TextBox.Text.IsNullOrEmpty() && !StartDate2TextBox.Text.IsNullOrEmpty() &&
                !EndDate2TextBox.Text.IsNullOrEmpty())
               dateRanges.Add(new DateRangeSearchParam.DateRangeField(FieldName2TextBox.Text,
                                                                      DateTime.Parse(StartDate2TextBox.Text),
                                                                      DateTime.Parse(EndDate2TextBox.Text)));

            return dateRanges;
         }
      }

      public override List<SkinnyItem> GetItems(string indexName,
                                                string language,
                                                string templateFilter,
                                                string locationFilter,
                                                string fullTextQuery)
      {
         var searchParam = new DateRangeSearchParam
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