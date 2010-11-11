using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Security.AccessControl;

namespace Sitecore.SharedSource.Search.DynamicFields
{
   public class HasExplicitDeniesField : BaseDynamicField
   {
      public override string ResolveValue(Item item)
      {
         Assert.ArgumentNotNull(item, "item");
         return HasExplicitDenies(item).ToString().ToLowerInvariant();
      }

      protected virtual bool HasExplicitDenies(Item item)
      {
         Assert.ArgumentNotNull(item, "item");

         foreach (var rule in item.Security.GetAccessRules())
         {
            if (rule.SecurityPermission == SecurityPermission.DenyAccess)
            {
               return true;
            }
         }

         return false;
      }
   }
}
