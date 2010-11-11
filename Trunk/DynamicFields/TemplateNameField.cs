using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace Sitecore.SharedSource.Search.DynamicFields
{
   public class TemplateNameField : BaseDynamicField
   {
      public override string ResolveValue(Item item)
      {
         Assert.ArgumentNotNull(item, "item");

         return item.Template.Name;
      }
   }
}
