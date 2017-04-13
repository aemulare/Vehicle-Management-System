using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Vms.Views
{
   public static class EnumHelper
   {
      /// <summary>
      /// Retrieves the collection of displayed names for the specified enumeration type.
      /// </summary>
      /// <typeparam name="T">The actual enumeration type.</typeparam>
      /// <returns>The collection of displayed names for the specified enumeration type.</returns>
      public static IEnumerable<string> GetEnumDisplayNames<T>()
         where T : struct
      {
         return from field in GetEnumFields(GetEnumType<T>()) select field.Name.Replace('_', ' ');
      }



      /// <summary>
      /// Retrieves a display name for the specified enumeration member.
      /// </summary>
      /// <param name="source">The source enumeration member.</param>
      /// <returns>The display name for the specified enumeration member.</returns>
      public static string GetDisplayName(this Enum source)
      {
         if(source == null)
            return "";

         var memberInfo = GetFieldInfo(source);
         return memberInfo?.Name.Replace('_', ' ') ?? "";
      }



      /// <summary>
      /// Retrieves the element of specified enumeration type by the displayed name.
      /// </summary>
      /// <typeparam name="T">The actual enumeration type.</typeparam>
      /// <param name="displayName">Enumeration element displayed name.</param>
      /// <returns>The element of the specified enumeration type.</returns>
      public static T? GetEnumElement<T>(string displayName)
         where T : struct
      {
         return ParseEnumValue<T>(displayName);
      }



      /// <summary>
      /// Retrieves the sequence of <see cref="FieldInfo"/> instances for the specified enumeration type.
      /// </summary>
      /// <param name="enumType">The actual enumeration type.</param>
      /// <returns>The sequence of <see cref="FieldInfo"/> instances.</returns>
      internal static IEnumerable<FieldInfo> GetEnumFields(IReflect enumType)
      {
         return enumType.GetFields(BindingFlags.Public | BindingFlags.Static);
      }



      /// <summary>
      /// Retrieves <see cref="FieldInfo"/> instance for the specified enumeration member.
      /// </summary>
      /// <param name="source">The source enumeration member.</param>
      /// <returns>The instance of <see cref="FieldInfo"/> class.</returns>
      internal static FieldInfo GetFieldInfo(Enum source)
      {
         var fields = GetEnumFields(source.GetType());
         return fields.FirstOrDefault(field => Equals(field.GetValue(null), source));
      }



      /// <summary>
      /// Retrieves the enum type object.
      /// </summary>
      /// <typeparam name="T">The actual enum type.</typeparam>
      /// <returns>The instance of <see cref="Type"/> class.</returns>
      private static Type GetEnumType<T>()
      {
         var enumType = typeof(T);
         VerifyEnumType(enumType);
         return enumType;
      }



      /// <summary>
      /// Performs check to ensure that the specified type is an enumeration.
      /// </summary>
      /// <param name="enumType">The type to be checked.</param>
      private static void VerifyEnumType(Type enumType)
      {
         if(!enumType.IsEnum)
            throw new ArgumentException($"The type '{enumType.Name}' must be a enumeration.");
      }



      /// <summary>
      /// Tries to parse the specified string value into the element of the specified enumeration type.
      /// </summary>
      /// <typeparam name="T">The type of enumeration.</typeparam>
      /// <param name="displayName">The string value representing the enumeration element name to parse.</param>
      /// <returns>The element of the specified enumeration type or null, if the specified name is not found.</returns>
      private static T? ParseEnumValue<T>(string displayName)
         where T : struct
      {
         return ((from field in GetEnumFields(typeof(T)) select field.Name).Any(n => n == displayName)
            ? (T?)Enum.Parse(typeof(T), displayName, true)
            : null);
      }
   }
}
