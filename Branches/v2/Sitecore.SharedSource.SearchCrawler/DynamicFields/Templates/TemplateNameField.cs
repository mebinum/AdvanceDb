using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace Sitecore.SharedSource.SearchCrawler.DynamicFields.Templates
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
