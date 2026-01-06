namespace Infrastructure.Result.Concrete
{
    public class SuccessResult : Result
    {
        public SuccessResult(string message) : base(true, message) { }

        // Mesajsız başarılı işlem
        public SuccessResult() : base(true) { }
    }
}
