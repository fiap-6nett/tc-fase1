namespace uBee.Shared.Commands
{
    public class GenericCommandResult : ICommandResult
    {
        #region Properties
        public bool Success { get; set; }
        public string Message { get; set; }
        public Object Data { get; set; }

        #endregion

        #region Constructors
        public GenericCommandResult(bool success, string message, object data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        #endregion
    }
}
