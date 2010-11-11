using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Diagnostics;

namespace Sitecore.SharedSource.Search.DynamicFields
{
   public class IsInWorkflowField : BaseDynamicField
   {
      public override string ResolveValue(Item item)
      {
         Assert.ArgumentNotNull(item, "item");
         return IsInWorkflow(item).ToString().ToLowerInvariant();
      }

      protected virtual bool IsInWorkflow(Item item)
      {
         Assert.ArgumentNotNull(item, "item");
         if (!TemplateManager.IsFieldPartOfTemplate(FieldIDs.Workflow, item))
         {
            return false;
         }
         var workflowProvider = item.Database.WorkflowProvider;
         if ((workflowProvider == null) || (workflowProvider.GetWorkflows().Length <= 0))
         {
            return false;
         }

         var workflow = workflowProvider.GetWorkflow(item);
         if (workflow == null)
         {
            return false;
         }

         var state = workflow.GetState(item);
         if (state == null)
         {
            return false;
         }

         return !state.FinalState;
      }
   }
}
