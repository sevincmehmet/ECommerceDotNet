namespace Infrastructure.Result.Concrete
{
    public class ErrorResult : Result
    {
        /// <summary>
        /// Mesajlı hatalı işlem
        /// </summary>
        /// <param name="message"></param>
        public ErrorResult(string message) : base(false, message) { }

        /// <summary>
        /// Mesajsız hatalı işlem
        /// </summary>
        public ErrorResult() : base(false) { }

    }

}
