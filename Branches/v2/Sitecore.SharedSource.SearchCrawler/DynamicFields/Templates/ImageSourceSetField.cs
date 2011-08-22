using System;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace Sitecore.SharedSource.SearchCrawler.DynamicFields.Templates
{
    public class ImageSourceSetField : BaseDynamicField
    {
        public override string ResolveValue(Item item)
        {
            Assert.ArgumentNotNull(item, "item");

            if (item.TemplateID.Equals(TemplateIDs.TemplateField))
            {
                if (item[TemplateFieldIDs.Type].Equals("image", StringComparison.InvariantCultureIgnoreCase))
                {
                    var field = item.Fields[TemplateFieldIDs.Source];

                    if (field != null && FieldTypeManager.GetField(field) is InternalLinkField)
                    {
                        var targetItem = ((InternalLinkField) field).TargetItem;

                        if (targetItem == null) return "0";

                        if (targetItem.Paths.IsMediaItem && targetItem.TemplateID.Equals(TemplateIDs.MediaFolder))
                            return "1";

                        return "0";
                    }
                }
            }

            return null;
        }
    }
}
