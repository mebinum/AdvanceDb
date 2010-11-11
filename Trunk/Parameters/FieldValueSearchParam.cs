using Sitecore.Collections;
using Sitecore.Search;

namespace Sitecore.SharedSource.Search.Parameters
{
   public class FieldValueSearchParam : SearchParam
   {
      public FieldValueSearchParam()
      {
         Refinements = new SafeDictionary<string>();
      }

      public QueryOccurance Occurance { get; set; }

      public SafeDictionary<string> Refinements { get; set; }
   }
}
