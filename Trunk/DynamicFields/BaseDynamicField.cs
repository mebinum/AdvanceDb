using Sitecore.Data.Items;
using Sitecore.SharedSource.Search.Utilities;

namespace Sitecore.SharedSource.Search.DynamicFields
{
   public abstract class BaseDynamicField : SearchField
   {
      public abstract string ResolveValue(Item item);

      public string FieldKey { get; set; }
   }
}
