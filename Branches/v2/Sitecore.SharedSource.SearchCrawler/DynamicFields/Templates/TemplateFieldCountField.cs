using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Diagnostics;
using Sitecore.SharedSource.Searcher.Utilities;

namespace Sitecore.SharedSource.SearchCrawler.DynamicFields.Templates
{
    public class TemplateFieldCountField : BaseDynamicField
    {
        public override string ResolveValue(Item item)
        {
            Assert.ArgumentNotNull(item, "item");

            if (TemplateManager.IsTemplate(item))
            {
                var template = TemplateManager.GetTemplate(item.ID, item.Database);

                return SearchHelper.FormatNumber(template.GetFields(false).Length);
            }

            return null;
        }
    }
}
