using Infrastructure.Result.Abstract;

namespace Infrastructure.Result.Concrete
{
    public class Result(bool success) : IResult
    {
        public bool Success { get; set; } = success;
        public string Message { get; set; }
        public Result(bool success, string message) : this(success)
        {
            Message = message;
        }
    }
}
