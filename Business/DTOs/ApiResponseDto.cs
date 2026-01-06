// ApiResponseDto<T>.cs
// T: İşlem başarılıysa dönen ana veri tipini temsil eder. (Örn: UserDto, List<ProductDto>)

namespace Business.DTOs
{
    public class ApiResponseDto<T>
    {
        // İşlemin başarılı olup olmadığını belirtir (HTTP kodu 2xx ise true)
        public bool Success { get; set; }

        // Hata mesajı veya başarı mesajı (eğer varsa)
        public string Message { get; set; }

        // HTTP durum kodunu doğrudan taşıyabiliriz. (Örn: 200, 404, 401)
        public int StatusCode { get; set; }

        // Asıl verinin bulunduğu yer. Hata durumunda null olacaktır.
        public T Data { get; set; }

        // Hatalar için ek bilgiler (örneğin, Validasyon hataları)
        public List<string> Errors { get; set; } = new List<string>();
    }
}