using Lucene.Net.Search;
using Sitecore.Search;

namespace Sitecore.SharedSource.Searcher.Parameters
{
   public interface ISearchParam
   {
      BooleanQuery ProcessQuery(QueryOccurance occurance, Index index);
   }
}