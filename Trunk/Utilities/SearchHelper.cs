#region Usings

using System.Collections.Generic;
using Lucene.Net.Documents;
using Sitecore.Data;
using Sitecore.Search;
using Sitecore.Data.Items;
using Sitecore.Collections;
using System;
using System.Linq;

#endregion

namespace Sitecore.SharedSource.Search.Utilities
{
   public class SearchHelper
   {
      public static string FormatNumber(int number)
      {
         return number.ToString().PadLeft(int.MaxValue.ToString().ToCharArray().Count(), '0');
      }

      public static List<Item> GetItemListFromInformationCollection(List<SkinnyItem> skinnyItems)
      {
         return skinnyItems.Select(skinnyItem => skinnyItem.GetItem()).ToList();
      }

      public static SafeDictionary<string> CreateRefinements(string fieldName, string fieldValue)
      {
         var refinements = new SafeDictionary<string>();

         if (!String.IsNullOrEmpty(fieldValue) && !String.IsNullOrEmpty(fieldValue))
         {
            if (fieldName.Contains("|"))
            {
               var fieldNames = fieldName.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

               foreach (var name in fieldNames)
               {
                  refinements.Add(name, fieldValue);
               }
            }
            else
            {
               refinements.Add(fieldName, fieldValue);
            }
         }

         return refinements;
      }

      public static void GetItemsFromSearchResult(IEnumerable<SearchResult> searchResults, List<SkinnyItem> items, bool showAllVersions)
      {
         foreach (var result in searchResults)
         {
            var uriField = result.Document.GetField(BuiltinFields.Url);
            if (uriField != null && !String.IsNullOrEmpty(uriField.StringValue()))
            {
               var itemUri = new ItemUri(uriField.StringValue());

               var itemInfo = new SkinnyItem(itemUri);

               foreach (Field field in result.Document.GetFields())
               {
                  itemInfo.Fields[field.Name()] = field.StringValue();
               }

               items.Add(itemInfo);
            }

            if (showAllVersions)
               GetItemsFromSearchResult(result.Subresults, items, true);
         }
      }

      /// <summary>
      /// Converts SearchResultCollection object into List<Item> collection.
      /// </summary>
      /// <param name="searchResults">SearchResultCollection object.</param>
      /// <returns></returns>
      //public static List<Item> GetItemsFromSearchResult(SearchResultCollection searchResults)
      //{
      //   var resultingSet = new List<Item>();

      //   foreach (var result in searchResults)
      //   {
      //      var itemObject = SearchManager.GetObject(result);

      //      if (itemObject != null && itemObject is Item)
      //      {
      //         resultingSet.Add(itemObject as Item);
      //      }
      //   }

      //   return resultingSet;
      //}

      //public static List<T> GetItemsFromSearchResult<T>(SearchResultCollection searchResults) where T : class
      //{
      //   var resultingSet = new List<T>();

      //   foreach (var result in searchResults)
      //   {
      //      var itemObject = SearchManager.GetObject(result);

      //      if (itemObject != null && itemObject is Item)
      //      {
      //         var item = itemObject as Item;
      //         // This is not gonna work unless T type is derived from "item"'s type.
      //         T source = (T)(object)item;
      //         resultingSet.Add(source);
      //      }
      //   }

      //   return resultingSet;
      //}
   }
}
