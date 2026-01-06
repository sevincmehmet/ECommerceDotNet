using Infrastructure.Result.Abstract;

namespace Infrastructure.Result.Concrete
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        // Hem veri, hem başarı durumu, hem de mesajı alır.
        public DataResult(T data, bool success, string message) : base(success, message)
        {
            Data = data;
        }

        // Veri ve başarı durumunu alır, mesajsız.
        public DataResult(T data, bool success) : base(success)
        {
            Data = data;
        }

        public T Data { get; }
    }
}