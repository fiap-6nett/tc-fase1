namespace uBee.Shared.Extensions
{
    public static class DateTimeExtensions
    {
        #region Extension Methods

        public static bool IsDefault(this DateTime source)
            => source == default;

        #endregion
    }
}
