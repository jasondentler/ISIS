using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ISIS
{
    public class EnumData<T>
    {

        private readonly IDictionary<int, string> _values;

        public EnumData()
        {
            var enumNames = GetNames();
            var enumValues = GetValues();
            var enumDict = enumValues
                .Zip(enumNames, (value, name) => new {value, name = GetDescription(name)})
                .ToDictionary(item => item.value, item => item.name);
            _values = enumDict;
        }

        private string GetDescription(string name)
        {
            var t = typeof (T);
            var fi = t.GetField(name);
            var attributes = (DescriptionAttribute[])
                             fi.GetCustomAttributes(typeof (DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : name;
        }

        private IEnumerable<string> GetNames()
        {
            var t = typeof (T);
            return Enum.GetNames(t);
        }

        private static IEnumerable<int> GetValues()
        {
            var t = typeof (T);
            return from object value in Enum.GetValues(t) select Convert.ToInt32(value);
        }

        public IDictionary<int, string> GetDictionary()
        {
            return _values;
        }

    }

    public static class EnumData
    {

        private static readonly ConcurrentDictionary<Type, IDictionary<int, string>> dictionaries;

        static EnumData()
        {
            dictionaries = new ConcurrentDictionary<Type, IDictionary<int, string>>();
        }

        public static IDictionary<int, string> GetEnumerableEnumValues(Type enumerableType)
        {
            if (!enumerableType.IsGenericType)
                throw new NotSupportedException(string.Format("{0} doesn't have generic parameters.", enumerableType));
            var enumType = enumerableType.GetGenericArguments().First();
            return GetEnumValues(enumType);
        }

        public static IDictionary<int, string> GetEnumValues(Type enumType)
        {
            return dictionaries.GetOrAdd(enumType, BuildDictionary);
        }

        private static IDictionary<int, string> BuildDictionary(Type enumType)
        {
            if (!enumType.IsEnum)
                throw new NotSupportedException(string.Format("{0} is not an enum type.", enumType));
            var enumDataType = typeof(EnumData<>).MakeGenericType(enumType);
            dynamic enumData = Activator.CreateInstance(enumDataType);
            return enumData.GetDictionary();
        }

    }

}
