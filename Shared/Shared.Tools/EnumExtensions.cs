using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace FileSys.Shared.CrossCutting.Tools
{
    public static class EnumExtensions
    {
        public static Dictionary<string, string> ToDictionary<TEnum>() where TEnum : struct
        {
            return System.Enum.GetValues(typeof(TEnum))
                .Cast<System.Enum>()
                .Select(item => new
                    {
                        key = (int)(object)item,
                        value = item.Descricao()
                    })
                .OrderBy(x => x.value)
                .ToDictionary(x => x.key.ToString(), x => x.value)
                ;
        }

        public static string Descricao(this System.Enum @enum)
        {

            var enumItem = @enum.GetType()
                            .GetMember(@enum.ToString())
                            .FirstOrDefault();

            if (enumItem == null)
                return string.Empty;

            var attribute = enumItem.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attribute == null || attribute.Length == 0)
                return @enum.ToString();

            return (attribute[0] as DescriptionAttribute).Description;
        }

        public static int ValorNumerico(this System.Enum @enum)
        {
            return Convert.ToInt16(@enum);
        }

        public static string Valor(this System.Enum @enum)
        {
            return Convert.ToInt16(@enum).ToString();
        }

        public static TEnum ToEnum<TEnum>(this string strEnumValue)
        {
            return (TEnum)Enum.Parse(typeof(TEnum), strEnumValue);
        }
    }
}
