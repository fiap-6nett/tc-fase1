namespace uBee.Domain.Core.Utility.Extensions
{
    public static class DateTimeExtension
    {
        #region Extension Methods

        public static bool IsDefault(this DateTime source)
            => source == default;

        #endregion
    }
}
