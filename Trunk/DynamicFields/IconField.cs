using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace Sitecore.SharedSource.Search.DynamicFields
{
   public class IconField : BaseDynamicField
   {
      public override string ResolveValue(Item item)
      {
         Assert.ArgumentNotNull(item, "item");
         return Sitecore.Resources.Themes.MapTheme(item.Appearance.Icon);
      }
   }
}
