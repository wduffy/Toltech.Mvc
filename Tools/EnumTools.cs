using System;
using System.Collections.Generic;
using System.Reflection;

namespace Toltech.Mvc.Tools
{

    public static class EnumTools
    {

        ///<summary>
        ///Gets a strongly typed Dictionary representation of an Enum
        ///</summary>
        ///<param name="enumType">The <see cref="System.Enum" /> type to be parsed</param>
        ///<param name="includeZeroKey">If true includes the zero based value of the enum in the new <see cref="Collections.Generic.Dictionary(Of Integer, string)" /></param>
        ///<returns>A <see cref="Collections.Generic.Dictionary(Of Integer, string)" /> which represents the specified <see cref="[Enum]" /></returns>
        ///<remarks>William Duffy: 2008-03-03</remarks>
        public static Dictionary<int, string> GetEnumAsDictionary(Type enumType, bool includeZeroKey)
        {
            if (enumType == null)
                throw new ArgumentNullException("enumType");
            
            if (!enumType.IsEnum)
                throw new ArgumentException("The argument passed is not of type Enum", "enumType");
            
            Dictionary<int, string> enumDictionary = new Dictionary<int,string>();

            foreach (FieldInfo fieldInfo in enumType.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                int key = (int)fieldInfo.GetValue(null);
                string value = fieldInfo.Name.Replace('_', ' ');

                if (key == 0 && !includeZeroKey)
                    continue;
                else
                    enumDictionary.Add(key, value);
            }
            
            return enumDictionary;
        }

    }

}