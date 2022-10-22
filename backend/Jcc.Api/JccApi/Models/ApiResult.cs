namespace JccApi.Models
{
    public class ApiResult<T>
    {
        public ApiResult(T data)
        {
            Data = data;
        }

        public T Data { get; private set; }
    }
}
