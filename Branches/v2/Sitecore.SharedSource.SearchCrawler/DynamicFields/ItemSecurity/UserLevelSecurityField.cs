using System.Linq;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Security.Accounts;

namespace Sitecore.SharedSource.SearchCrawler.DynamicFields.ItemSecurity
{
   public class UserLevelSecurityField : BaseDynamicField
   {
      public override string ResolveValue(Item item)
      {
         Assert.ArgumentNotNull(item, "item");
         return item.Security.GetAccessRules().Select(rule => rule.Account.AccountType == AccountType.User ? "1" : "0").FirstOrDefault();
      }
   }
}
