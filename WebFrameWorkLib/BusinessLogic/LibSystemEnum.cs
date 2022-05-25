using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebFrameWorkLib.BusinessLogic
{
    public class LibSystemEnum
    {
        public enum roleDeleteStatus
        {
            [Description("NotDeleted")]
            active = 0,
            [Description("Deleted")]
            inactive = 1
        }

        public enum userAccessStatus
        {
            [Description("Active")]
            active = 0,
            [Description("Inactive")]
            inactive = 1
        }

        public enum userDeleteStatus
        {
            [Description("NotDeleted")]
            active = 0,
            [Description("Deleted")]
            inactive = 1
        }

        public enum emailDeleteStatus
        {
            [Description("NotDeleted")]
            active = 0,
            [Description("Deleted")]
            inactive = 1
        }

        public enum emailStatus
        {
            [Description("Active")]
            active = 0,
            [Description("Inactive")]
            inactive = 1
        }

        public enum userProfileStatus
        {
            [Description("Active")]
            active = 0,
            [Description("Inactive")]
            inactive = 1
        }

        public enum userProfileDeleteStatus
        {
            [Description("NotDeleted")]
            active = 0,
            [Description("Deleted")]
            inactive = 1
        }

        public enum moduleStatus
        {
            [Description("Active")]
            active = 0,
            [Description("Inactive")]
            inactive = 1
        }

        public enum roleStatus
        {
            [Description("Active")]
            active = 0,
            [Description("Inactive")]
            inactive = 1
        }

        public enum tabStatus
        {
            [Description("Active")]
            active = 0,
            [Description("Inactive")]
            inactive = 1
        }

        public enum userStatus
        {
            [Description("Active")]
            active = 0,
            [Description("Inactive")]
            inactive = 1
        }

        public enum userGroupDeleteStatus
        {
            [Description("NotDeleted")]
            active = 0,
            [Description("Deleted")]
            inactive = 1
        }

    }

    public class LibEnumDescription
    {
        /// <summary>
        /// Get the description attribute for one enum value
        /// </summary>
        /// <param name="value">>Enum value
        /// <returns>The description attribute of an enum, if any</returns>
        public static string GetDescription(Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }

        /// <summary>
        /// Gets a list of key/value pairs for an enum, using the description attribute as value
        /// </summary>
        /// <param name="enumType">>typeof(your enum type)
        /// <returns>A list of KeyValuePairs with enum values and descriptions</returns>
        public static List<KeyValuePair<string, string>> GetValuesAndDescription(System.Type enumType)
        {
            List<KeyValuePair<string, string>> kvPairList = new List<KeyValuePair<string, string>>();

            foreach (Enum enumValue in Enum.GetValues(enumType))
            {
                kvPairList.Add(new KeyValuePair<string, string>(enumValue.ToString(), GetDescription(enumValue)));
            }

            return kvPairList;
        }

        public static List<KeyValuePair<string, string>> GetValuesIntAndDescription(System.Type enumType)
        {
            List<KeyValuePair<string, string>> kvPairList = new List<KeyValuePair<string, string>>();

            foreach (Enum enumValue in Enum.GetValues(enumType))
            {
                kvPairList.Add(new KeyValuePair<string, string>(enumValue.ToString("D"), GetDescription(enumValue)));
            }

            return kvPairList;
        }

        public static T GetValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException("Not found.", "description");
            // or return default(T);
        }
    }
}
