using System;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Sitecore.Search;
using Sitecore.SharedSource.Searcher.Utilities;

namespace Sitecore.SharedSource.Searcher.Parameters
{
   public class FieldSearchParam : SearchParam
   {
      public string FieldName { get; set; }

      public override BooleanQuery ProcessQuery(QueryOccurance occurance, Index index)
      {
         var query = base.ProcessQuery(occurance, index) ?? new BooleanQuery();
         AddPartialFieldValueClause(index, query, FieldName, RelatedIds);
         return query;
      }

      protected void AddPartialFieldValueClause(Index index, BooleanQuery query, string fieldName, string fieldValue)
      {
         if (String.IsNullOrEmpty(fieldValue)) return;

         fieldValue = IdHelper.ProcessGUIDs(fieldValue);

         var fullTextQuery = new QueryParser(fieldName, index.Analyzer).Parse(fieldValue);

         query.Add(fullTextQuery, BooleanClause.Occur.MUST);
      }
   }
}
