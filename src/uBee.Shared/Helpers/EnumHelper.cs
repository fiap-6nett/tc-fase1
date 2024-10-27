using System.ComponentModel;
using System.Reflection;

namespace uBee.Shared.Helpers
{
    public static class EnumHelper<T> where T : Enum
    {
        #region Read-Only Fields

        private static readonly Dictionary<T, string> Descriptions;

        #endregion

        #region Static Constructor

        static EnumHelper()
        {
            Descriptions = Enum.GetValues(typeof(T))
                .Cast<T>()
                .ToDictionary(value => value, GetEnumDescription);
        }

        #endregion

        #region Private Methods

        private static string GetEnumDescription(T value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            if (fieldInfo is null)
            {
                return value.ToString();
            }

            var descriptionAttribute = fieldInfo.GetCustomAttribute<DescriptionAttribute>();

            return descriptionAttribute?.Description ?? value.ToString();
        }

        #endregion

        #region Public Methods

        public static string GetDescription(T value)
        {
            return Descriptions.TryGetValue(value, out var description)
                ? description
                : value.ToString();
        }

        public static bool TryConvert<TEnum>(object value, out TEnum result) where TEnum : Enum
        {
            result = default;

            if (value is null || !Enum.IsDefined(typeof(TEnum), value))
            {
                return false;
            }

            result = (TEnum)Enum.ToObject(typeof(TEnum), value);
            return true;
        }

        #endregion
    }
}
