﻿using System;
using System.Linq;
using Lucene.Net.Search;
using Sitecore.Data;
using Sitecore.Search;
using Sitecore.SharedSource.Searcher.Utilities;

namespace Sitecore.SharedSource.Searcher.Parameters
{
   public class SearchParam: ISearchParam
   {
      public string RelatedIds { get; set; }
      public string TemplateIds { get; set; }
      public string LocationIds { get; set; }
      public string FullTextQuery { get; set; }
      public string Language { get; set; }
      public QueryOccurance Condition { get; set; }

      public SearchParam()
      {
         Condition = QueryOccurance.Must;
      }

      public virtual BooleanQuery ProcessQuery(QueryOccurance occurance, Index index)
      {
         var innerQuery = new CombinedQuery();

         ApplyLanguageClause(innerQuery, Language, occurance);
         ApplyFullTextClause(innerQuery, FullTextQuery, occurance);
         ApplyRelationFilter(innerQuery, RelatedIds, occurance);
         ApplyTemplateFilter(innerQuery, TemplateIds, occurance);
         ApplyLocationFilter(innerQuery, LocationIds, occurance);

         if(innerQuery.Clauses.Count < 1)
            return null;

         var translator = new QueryTranslator(index);
         var booleanQuery = translator.ConvertCombinedQuery(innerQuery);

         return booleanQuery;
      }

      protected void ApplyLanguageClause(CombinedQuery query, string language, QueryOccurance occurance)
      {
         if (String.IsNullOrEmpty(language)) return;

         query.Add(new FieldQuery(BuiltinFields.Language, language.ToLowerInvariant()), occurance);
      }

      protected void ApplyFullTextClause(CombinedQuery query, string searchText, QueryOccurance occurance)
      {
         if (String.IsNullOrEmpty(searchText)) return;

         query.Add(new FullTextQuery(searchText), occurance);
      }

      protected void ApplyIdFilter(CombinedQuery query, string fieldName, string filter, QueryOccurance occurance)
      {
         if (String.IsNullOrEmpty(fieldName) || String.IsNullOrEmpty(filter)) return;

         var filterQuery = new CombinedQuery();

         var values = IdHelper.ParseId(filter);

         foreach (var value in values.Where(ID.IsID))
         {
            AddFieldValueClause(filterQuery, fieldName, value, QueryOccurance.Should);
         }

         query.Add(filterQuery, occurance);
      }

      protected void ApplyTemplateFilter(CombinedQuery query, string templateIds, QueryOccurance occurance)
      {
         if (String.IsNullOrEmpty(templateIds)) return;

         templateIds = IdHelper.NormalizeGuid(templateIds);
         var fieldQuery = new FieldQuery(BuiltinFields.Template, templateIds);
         query.Add(fieldQuery, occurance);
      }

      protected void ApplyLocationFilter(CombinedQuery query, string locationIds, QueryOccurance occurance)
      {
         ApplyIdFilter(query, BuiltinFields.Path, locationIds, occurance);
      }

      protected void ApplyRelationFilter(CombinedQuery query, string ids, QueryOccurance occurance)
      {
         ApplyIdFilter(query, BuiltinFields.Links, ids, occurance);
      }

      protected void AddFieldValueClause(CombinedQuery query, string fieldName, string fieldValue, QueryOccurance occurance)
      {
         if (String.IsNullOrEmpty(fieldName) || String.IsNullOrEmpty(fieldValue)) return;

         // if we are searching by _id field, do not lowercase
         fieldValue = IdHelper.ProcessGUIDs(fieldValue);
         query.Add(new FieldQuery(fieldName, fieldValue), occurance);
      }
   }
}