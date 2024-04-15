namespace Shared.SeedWork
{
    public class ApiResult<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get;set; }
        public T Data { get; }

        public ApiResult() 
        {

        }

        public ApiResult(bool isSuccess , string message = null)
        {
            Message = message;
            IsSuccess = isSuccess;
        }

        public ApiResult(bool isSuccess,T data, string message = null)
        {
            Data = data;
            Message = message;
            IsSuccess = isSuccess;
        }    
    }
}
