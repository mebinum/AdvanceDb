using System;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Diagnostics;
using Sitecore.Workflows;

namespace Sitecore.SharedSource.Search.DynamicFields
{
   public class WorkflowStateField : BaseDynamicField
   {
      public override string ResolveValue(Item item)
      {
         Assert.ArgumentNotNull(item, "item");
         return GetWorkflowStateName(item).ToLowerInvariant();
      }

      public static string GetWorkflowStateName(Item item)
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
         if (workflow == null)
         {
            return String.Empty;
         }

         var state = workflow.GetState(item);
         if (state != null)
         {
            return state.DisplayName;
         }

         return String.Empty;
      }
   }
}
