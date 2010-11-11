using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.SecurityModel;

namespace Sitecore.SharedSource.Search.DynamicFields
{
   public class IsApprovedField : BaseDynamicField
   {
      public override string ResolveValue(Item item)
      {
         Assert.ArgumentNotNull(item, "item");
         return IsApproved(item).ToString().ToLowerInvariant();
      }

      protected virtual bool IsApproved(Item item)
      {
         var stateItem = GetStateItem(item);
         if (stateItem != null)
         {
            return stateItem[WorkflowFieldIDs.FinalState] == "1";
         }

         return false;
      }

      public static Item GetStateItem(Item item)
      {
         var stateID = GetStateID(item);
         if (stateID.Length > 0)
         {
            return GetStateItem(stateID, item.Database);
         }
         return null;
      }

      public static Item GetStateItem(string stateId, Database database)
      {
         var iD = MainUtil.GetID(stateId, null);
         if (iD.IsNull)
         {
            return null;
         }
         return GetItem(iD, database);
      }

      public static Item GetItem(ID itemId, Database database)
      {
         return ItemManager.GetItem(itemId, Language.Current, Version.Latest, database, SecurityCheck.Disable);
      }

      public static string GetStateID(Item item)
      {
         Assert.ArgumentNotNull(item, "item");
         var workflowInfo = item.Database.DataManager.GetWorkflowInfo(item);
         if (workflowInfo != null)
         {
            return workflowInfo.StateID;
         }
         return string.Empty;
      }
   }
}
