using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Sitecore.Data.Items;
using Sitecore.Data.Validators;
using Sitecore.Diagnostics;

namespace Sitecore.SharedSource.SearchCrawler.DynamicFields.Content
{
   public static class EnumExtension
   {
      public static string GetDescription<T>(this object enumerationValue) where T : struct
      {
         Type type = enumerationValue.GetType();
         if (!type.IsEnum)
         {
            throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
         }

         //Tries to find a DescriptionAttribute for a potential friendly name
         //for the enum
         MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
         if (memberInfo != null && memberInfo.Length > 0)
         {
            object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attrs != null && attrs.Length > 0)
            {
               //Pull out the description value
               return ((DescriptionAttribute)attrs[0]).Description;
            }
         }
         //If we have no description attribute, just return the ToString of the enum
         return enumerationValue.ToString();

      }
   }

   public class ValidationErrorsField : BaseDynamicField
   {
      public override string ResolveValue(Item item)
      {
         Assert.ArgumentNotNull(item, "item");

         var allvalidators = new ValidatorCollection();

         foreach (BaseValidator validator in ValidatorManager.BuildValidators(ValidatorsMode.ValidateButton, item))
         {
            allvalidators.Add(validator);
         }

         foreach (BaseValidator validator in ValidatorManager.BuildValidators(ValidatorsMode.ValidatorBar, item))
         {
            allvalidators.Add(validator);
         }

         foreach (BaseValidator validator in ValidatorManager.BuildValidators(ValidatorsMode.Workflow, item))
         {
            allvalidators.Add(validator);
         }

         foreach (BaseValidator validator in ValidatorManager.BuildValidators(ValidatorsMode.Gutter, item))
         {
            allvalidators.Add(validator);
         }

         ValidatorManager.Validate(allvalidators, new ValidatorOptions(false));

         var validationResult = new List<string>();

         foreach (BaseValidator validator in allvalidators)
         {
            validationResult.Add(validator.MaxValidatorResult.GetDescription<ValidatorResult>());
         }

         return String.Join("|", validationResult.ToArray());
      }
   }
}
