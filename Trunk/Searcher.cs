#region usings
using System;
using System.Collections.Generic;
using System.Linq;
using Lucene.Net.Search;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using Sitecore.Search;
using Sitecore.Diagnostics;
using Sitecore.Data;
using Sitecore.Collections;
using Sitecore.SharedSource.Search.Constants;
using Sitecore.SharedSource.Search.Parameters;
using Sitecore.SharedSource.Search.Utilities;

#endregion

namespace Sitecore.SharedSource.Search
{
   public class Searcher : IDisposable
   {
      #region ctor

      public Searcher(string indexId)
      {
         Index = SearchManager.GetIndex(indexId);
      }

      #endregion ctor

      #region Properties

      public Index Index { get; set; }

      #endregion Properties

      #region Query Runner Methods

      public virtual List<SkinnyItem> RunQuery(Query query)
      {
         return RunQuery(query, false);
      }

      public virtual List<SkinnyItem> RunQuery(Query query, bool showAllVersions)
      {
         Assert.ArgumentNotNull(Index, "Sitecore.SharedSource.Search");

         var items = new List<SkinnyItem>();

         try
         {
            using (var context = new IndexSearchContext(Index))
            {
               var hits = context.Search(query);

               if (hits == null)
               {
                  return null;
               }

               var resultCollection = hits.FetchResults(0, hits.Length);
               SearchHelper.GetItemsFromSearchResult(resultCollection, items, showAllVersions);
            }
         }
         catch (Exception exception)
         {
            Log.Error("Sitecore.SharedSource.Search. There was a problem while running a search query. Details: " + exception.Message, this);
            Log.Error(exception.StackTrace, this);
            throw;
         }

         return items;
      }

      public virtual List<SkinnyItem> RunQuery(QueryBase query, bool showAllVersions)
      {
         var translator = new QueryTranslator(Index);
         var luceneQuery = translator.Translate(query);
         return RunQuery(luceneQuery, showAllVersions);
      }

      public virtual List<SkinnyItem> RunQuery(QueryBase query)
      {
         return RunQuery(query, false);
      }

      #endregion

      #region Searching Methods

      public virtual List<SkinnyItem> GetItems(SearchParam param)
      {
         var globalQuery = new CombinedQuery();

         ApplyLanguageClause(globalQuery, param.Language);
         ApplyFullTextClause(globalQuery, param.FullTextQuery);
         ApplyRelationFilter(globalQuery, param.RelatedIds);
         ApplyTemplateFilter(globalQuery, param.TemplateIds);
         ApplyLocationFilter(globalQuery, param.LocationIds);

         return RunQuery(globalQuery, param.ShowAllVersions);
      }

      public virtual List<SkinnyItem> GetItems(FieldValueSearchParam param)
      {
         var globalQuery = new CombinedQuery();

         ApplyLanguageClause(globalQuery, param.Language);
         ApplyFullTextClause(globalQuery, param.FullTextQuery);
         ApplyRelationFilter(globalQuery, param.RelatedIds);
         ApplyTemplateFilter(globalQuery, param.TemplateIds);
         ApplyLocationFilter(globalQuery, param.LocationIds);
         ApplyRefinements(globalQuery, param.Refinements, param.Occurance);

         return RunQuery(globalQuery, param.ShowAllVersions);
      }

      //public virtual List<SkinnyItem> GetRelatedItemsByField(FieldSearchParam param)
      //{
      //   //string relatedIds, string templateIds, string fieldName, bool partial
      //   var globalQuery = new CombinedQuery();

      //   ApplyTemplateFilter(globalQuery, param.TemplateIds);

      //   var translator = new QueryTranslator(Index);
      //   var booleanQuery = translator.ConvertCombinedQuery(globalQuery);
      //   AddPartialFieldValueClause(booleanQuery, param.FieldName, param.RelatedIds);

      //   return RunQuery(booleanQuery, param.ShowAllVersions);
      //}

      public virtual bool ContainsItemsByFields(string ids, string fieldName, string fieldValue)
      {
         var globalQuery = new CombinedQuery();

         ApplyIdFilter(globalQuery, BuiltinFields.ID, ids);
         AddFieldValueClause(globalQuery, fieldName, fieldValue, QueryOccurance.Must);

         return RunQuery(globalQuery).Count > 0;
      }

      public virtual List<SkinnyItem> GetItemsInRange(NumericRangeSearchParam param)
      {
         var globalQuery = new CombinedQuery();

         ApplyLanguageClause(globalQuery, param.Language);
         ApplyTemplateFilter(globalQuery, param.TemplateIds);
         ApplyLocationFilter(globalQuery, param.LocationIds);
         ApplyFullTextClause(globalQuery, param.FullTextQuery);

         var translator = new QueryTranslator(Index);
         var booleanQuery = translator.ConvertCombinedQuery(globalQuery);
         var innerOccurance = translator.GetOccur(param.Occurance);

         ApplyNumericRangeSearchParam(booleanQuery, param, innerOccurance);

         return RunQuery(booleanQuery, param.ShowAllVersions);
      }

      public virtual List<SkinnyItem> GetItemsInRange(DateRangeSearchParam param)
      {
         var globalQuery = new CombinedQuery();

         ApplyLanguageClause(globalQuery, param.Language);
         ApplyTemplateFilter(globalQuery, param.TemplateIds);
         ApplyLocationFilter(globalQuery, param.LocationIds);
         ApplyFullTextClause(globalQuery, param.FullTextQuery);

         var translator = new QueryTranslator(Index);
         var booleanQuery = translator.ConvertCombinedQuery(globalQuery);
         var innerOccurance = translator.GetOccur(param.Occurance);

         ApplyDateRangeSearchParam(booleanQuery, param, innerOccurance);

         return RunQuery(booleanQuery, param.ShowAllVersions);
      }

      #endregion

      #region Clause Construction Helpers

      protected void ApplyNumericRangeSearchParam(BooleanQuery query, NumericRangeSearchParam param, BooleanClause.Occur innerOccurance)
      {
         if (param.Ranges.Count <= 0) return;

         foreach (var range in param.Ranges)
         {
            AddNumericRangeQuery(query, range, innerOccurance);
         }
      }

      protected void AddNumericRangeQuery(BooleanQuery query, NumericRangeSearchParam.NumericRangeField range, BooleanClause.Occur occurance)
      {
         var startTerm = new Term(range.FieldName, SearchHelper.FormatNumber(range.Start));
         var endTerm = new Term(range.FieldName, SearchHelper.FormatNumber(range.End));
         var rangeQuery = new RangeQuery(startTerm, endTerm, true);
         query.Add(rangeQuery, occurance);
      }

      protected void ApplyDateRangeSearchParam(BooleanQuery query, DateRangeSearchParam param, BooleanClause.Occur innerOccurance)
      {
         if (param.Ranges.Count <= 0) return;

         foreach (var dateParam in param.Ranges)
         {
            AddDateRangeQuery(query, dateParam, innerOccurance);
         }
      }

      protected void AddDateRangeQuery(BooleanQuery query, DateRangeSearchParam.DateRangeField dateRangeField, BooleanClause.Occur occurance)
      {
         var startDateTime = dateRangeField.StartDate;
         if (dateRangeField.InclusiveStart)
         {
            startDateTime = startDateTime.AddDays(1);
         }
         var startDate = startDateTime.ToString(IndexConstants.DateTimeFormat);

         var endDateTime = dateRangeField.EndDate;
         if (dateRangeField.InclusiveStart)
         {
            endDateTime = endDateTime.AddDays(1);
         }
         var endDate = endDateTime.ToString(IndexConstants.DateTimeFormat);

         var rangeQuery = new RangeQuery(new Term(dateRangeField.FieldName, startDate), new Term(dateRangeField.FieldName, endDate), true);
         query.Add(rangeQuery, occurance);
      }

      protected void ApplyRefinements(CombinedQuery query, SafeDictionary<string> refinements, QueryOccurance occurance)
      {
         if (refinements.Count <= 0) return;

         var innerQuery = new CombinedQuery();

         foreach (var refinement in refinements)
         {
            var fieldName = refinement.Key.ToLowerInvariant();
            var fieldValue = refinement.Value.ToLowerInvariant();
            AddFieldValueClause(innerQuery, fieldName, fieldValue, occurance);
         }

         query.Add(innerQuery, QueryOccurance.Must);
      }

      protected void AddFieldValueClause(CombinedQuery query, string fieldName, string fieldValue, QueryOccurance occurance)
      {
         if (String.IsNullOrEmpty(fieldName) || String.IsNullOrEmpty(fieldValue)) return;

         // if we are searching by _id field, do not lowercase
         fieldValue = IdHelper.ProcessGUIDs(fieldValue);
         query.Add(new FieldQuery(fieldName, fieldValue), occurance);
      }

      protected void AddPartialFieldValueClause(BooleanQuery query, string fieldName, string fieldValue)
      {
         if (String.IsNullOrEmpty(fieldValue)) return;

         fieldValue = IdHelper.ProcessGUIDs(fieldValue);

         var fullTextQuery = new QueryParser(fieldName, Index.Analyzer).Parse(fieldValue);

         query.Add(fullTextQuery, BooleanClause.Occur.MUST);
      }

      protected void ApplyLanguageClause(CombinedQuery query, string language)
      {
         if (String.IsNullOrEmpty(language)) return;

         query.Add(new FieldQuery(BuiltinFields.Language, language.ToLowerInvariant()), QueryOccurance.Must);
      }

      protected void ApplyFullTextClause(CombinedQuery query, string searchText)
      {
         if (String.IsNullOrEmpty(searchText)) return;

         query.Add(new FullTextQuery(searchText), QueryOccurance.Must);
      }

      protected void ApplyIdFilter(CombinedQuery query, string fieldName, string filter)
      {
         if (String.IsNullOrEmpty(fieldName) || String.IsNullOrEmpty(filter)) return;

         var filterQuery = new CombinedQuery();

         var values = IdHelper.ParseId(filter);

         foreach (var value in values.Where(ID.IsID))
         {
            AddFieldValueClause(filterQuery, fieldName, value, QueryOccurance.Should);
         }

         query.Add(filterQuery, QueryOccurance.Must);
      }

      protected void ApplyTemplateFilter(CombinedQuery query, string templateIds)
      {
         if (String.IsNullOrEmpty(templateIds)) return;

         templateIds = IdHelper.NormalizeGuid(templateIds);
         var fieldQuery = new FieldQuery(BuiltinFields.Template, templateIds);
         query.Add(fieldQuery, QueryOccurance.Must);
      }

      protected void ApplyLocationFilter(CombinedQuery query, string locationIds)
      {
         ApplyIdFilter(query, BuiltinFields.Path, locationIds);
      }

      protected void ApplyRelationFilter(CombinedQuery query, string ids)
      {
         ApplyIdFilter(query, BuiltinFields.Links, ids);
      }

      #endregion

      #region static scope

      /// <summary>
      /// Returns a search index by specified index id
      /// </summary>
      /// <param name="indexId">Search index id</param>
      /// <returns>Search index object</returns>
      public static Index GetIndex(string indexId)
      {
         return SearchManager.GetIndex(indexId);
      }

      #endregion static scope

      #region IDisposable Members

      public void Dispose()
      {
         if (Index != null)
         {
            Index = null;
         }
      }

      #endregion
   }
}
