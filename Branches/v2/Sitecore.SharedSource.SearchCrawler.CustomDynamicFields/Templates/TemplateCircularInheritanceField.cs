using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace Sitecore.SharedSource.SearchCrawler.DynamicFields.Templates
{
   public class TemplateCircularInheritanceField : BaseDynamicField
   {
      public override string ResolveValue(Item item)
      {
         Assert.ArgumentNotNull(item, "item");

         var template = item.Database.Engines.TemplateEngine.GetTemplate(item.ID);

         if(template != null)
         {
            return template.DescendsFrom(template) ? "1" : "0";
         }

         return "0";
      }
   }
}
