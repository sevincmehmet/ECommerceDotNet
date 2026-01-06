namespace Infrastructure.Result.Concrete
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        // Veri ve mesajlı başarılı sonuç
        public SuccessDataResult(T data, string message) : base(data, true, message) { }

        // Sadece veri (mesaj yok)
        public SuccessDataResult(T data) : base(data, true) { }

        // Default veri tipi için başarılı sonuç (örneğin T'nin varsayılan değeri)
        public SuccessDataResult(string message) : base(default, true, message) { }

        // Hiçbir şey yok
        public SuccessDataResult() : base(default, true) { }
    }

}