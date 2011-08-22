using System.Linq;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Security.AccessControl;

namespace Sitecore.SharedSource.SearchCrawler.DynamicFields.ItemSecurity
{
    public class HasExplicitDeniesField : BaseDynamicField
    {
        public override string ResolveValue(Item item)
        {
            Assert.ArgumentNotNull(item, "item");
            return item.Security.GetAccessRules().Any(rule => rule.SecurityPermission == SecurityPermission.DenyAccess) ? "1" : "0";
        }
    }
}
