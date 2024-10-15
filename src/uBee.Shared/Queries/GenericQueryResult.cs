namespace uBee.Shared.Queries
{
    public class GenericQueryResult : IQueryResult
    {
        #region Properties
        public bool Success { get; set; }
        public string Message { get; set; }
        public Object Data { get; set; }

        #endregion

        #region Constructors
        public GenericQueryResult(bool success, string message, object data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        #endregion
    }
}
