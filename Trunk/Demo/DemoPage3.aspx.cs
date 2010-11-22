namespace SearchDemo.Scripts
{
   using System.Collections.Generic;
   using Sitecore.Collections;
   using Sitecore.Search;
   using Sitecore.SharedSource.Search;
   using Sitecore.SharedSource.Search.Parameters;
   using Sitecore.SharedSource.Search.Utilities;
   using System.Web.UI.WebControls;
   using Sitecore.StringExtensions;

   public class DemoPage3 : DemoPage2
   {
      #region Fields

      protected TextBox FieldName1TextBox;
      protected TextBox FieldValue1TextBox;
      protected TextBox FieldName2TextBox;
      protected TextBox FieldValue2TextBox;

      #endregion

      protected SafeDictionary<string> GetRefinements()
      {
         var refinements = new SafeDictionary<string>();

         if (!FieldName1TextBox.Text.IsNullOrEmpty() && !FieldValue1TextBox.Text.IsNullOrEmpty())
            refinements.Add(FieldName1TextBox.Text, FieldValue1TextBox.Text);

         if (!FieldName2TextBox.Text.IsNullOrEmpty() && !FieldValue2TextBox.Text.IsNullOrEmpty())
            refinements.Add(FieldName2TextBox.Text, FieldValue2TextBox.Text);

         return refinements;
      }

      public override List<SkinnyItem> GetItems(string indexName,
                                                string language,
                                                string templateFilter,
                                                string locationFilter,
                                                string fullTextQuery)
      {
         var refinements = GetRefinements();

         var searchParam = new FieldValueSearchParam
                              {
                                 Refinements = refinements,
                                 LocationIds = locationFilter,
                                 TemplateIds = templateFilter,
                                 FullTextQuery = fullTextQuery,
                                 Occurance = QueryOccurance.Must,
                                 ShowAllVersions = false,
                                 Language = language
                              };

         using (var searcher = new Searcher(indexName))
         {
            return searcher.GetItems(searchParam);
         }
      }
   }
}