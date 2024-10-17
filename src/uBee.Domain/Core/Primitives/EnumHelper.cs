using System.ComponentModel;
using System.Reflection;

namespace uBee.Domain.Core.Primitives
{
    public static class EnumHelper<T> where T : Enum
    {
        private static readonly Dictionary<T, string> _descriptions = new Dictionary<T, string>();

        static EnumHelper()
        {
            foreach (var value in Enum.GetValues(typeof(T)).Cast<T>())
            {
                var description = GetEnumDescription(value);
                _descriptions[value] = description;
            }
        }

        private static string GetEnumDescription(T value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            if (fi != null)
            {
                var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes.Length > 0)
                {
                    return attributes[0].Description;
                }
            }

            return value.ToString();
        }

        public static string GetDescription(T value)
        {
            return _descriptions.TryGetValue(value, out var description) ? description : value.ToString();
        }
    }
}
