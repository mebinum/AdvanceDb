using System;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Diagnostics;

namespace Sitecore.SharedSource.Search.DynamicFields
{
   public class WorkflowField : BaseDynamicField
   {
      public override string ResolveValue(Item item)
      {
         Assert.ArgumentNotNull(item, "item");
         return GetWorkflowName(item).ToLowerInvariant();
      }

      public static string GetWorkflowName(Item item)
      {
         Assert.ArgumentNotNull(item, "item");
         if (!TemplateManager.IsFieldPartOfTemplate(FieldIDs.Workflow, item))
         {
            return String.Empty;
         }

         var workflowProvider = item.Database.WorkflowProvider;
         if ((workflowProvider == null) || (workflowProvider.GetWorkflows().Length <= 0))
         {
            return String.Empty;
         }

         var workflow = workflowProvider.GetWorkflow(item);
         if (workflow != null)
         {
            return workflow.Appearance.DisplayName;
         }

         return String.Empty;
      }
   }
}
