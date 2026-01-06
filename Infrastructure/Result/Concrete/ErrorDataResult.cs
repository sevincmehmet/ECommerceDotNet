
namespace Infrastructure.Result.Concrete
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        // Veri ve mesajlı hatalı sonuç
        public ErrorDataResult(T data, string message) : base(data, false, message) { }

        // Sadece veri (mesaj yok)
        public ErrorDataResult(T data) : base(data, false) { }

        // Default veri tipi ve mesaj
        public ErrorDataResult(string message) : base(default, false, message) { }

        // Hiçbir şey yok
        public ErrorDataResult() : base(default, false) { }
    }
}


