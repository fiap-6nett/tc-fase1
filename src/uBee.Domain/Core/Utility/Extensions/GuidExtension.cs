namespace uBee.Domain.Core.Utility.Extensions
{
    public static class GuidExtension
    {
        #region Extension Methods

        public static bool IsEmpty(this Guid source)
            => source == Guid.Empty;

        #endregion
    }
}
