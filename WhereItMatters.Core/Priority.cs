using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WhereItMatters.Core
{
    public enum Priority
    {
        [Description("Saves limmediate lives")]
        L1Max,
        [Description("Prevents spread of disease")]
        L2,
        [Description("Deals with food security")]
        L3,
        [Description("Education")]
        L4,
        [Description("Builds infrastructure")]
        L5,
        [Description("Improves social welfare")]
        L6Min
    }

    public static class EnumExtensions
    {
        public static string GetEnumDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}
