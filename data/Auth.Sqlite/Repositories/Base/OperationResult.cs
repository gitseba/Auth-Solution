namespace Auth.Sqlite.Repositories.Base
{
    /// <summary>
    /// Role of this class is to map a consistent response from db executions
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class OperationResult<T>
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public T Result { get; set; }
    }
}
