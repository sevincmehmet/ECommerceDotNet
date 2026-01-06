using Infrastructure.Security.Abstract;

namespace Infrastructure.Security.Concrete
{
    public class SecurityHelper : ISecurityHelper
    {
        // BCrypt ile Şifre Hashleme
        // BCrypt, tuzu ve iterasyon maliyetini hash çıktısının içine kaydeder, 
        // bu nedenle ekstra bir "salt" alanı tutmanıza gerek kalmaz.

        /// <summary>
        /// BCrypt kullanarak şifreyi tuzlayıp hashler.
        /// </summary>
        /// <param name="password">Hashlenecek düz metin şifre.</param>
        /// <returns>Tuz, maliyet ve Hash'i içeren tek bir string (BCrypt formatında).</returns>
        public string HashPassword(string password)
        {
            // Work Factor (Maliyet): Hesaplama süresini belirler. 
            // Varsayılan (10) çoğu senaryo için yeterince güvenli ve dengelidir.
            // Yüksek bir sayı daha yavaş ve daha güvenli hashleme demektir.
            const int workFactor = 12; // 10 yerine 12 seçmek daha yavaş ve güvenlidir.

            // BCrypt'in HashPassword metodu rastgele bir tuz oluşturur, 
            // şifreyi hashler ve tuzu da içeren çıktıyı tek bir string olarak döndürür.
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor);
        }

        /// <summary>
        /// Kullanıcının girdiği şifreyi, kayıtlı BCrypt hash'i ile doğrular.
        /// </summary>
        /// <param name="password">Kullanıcının girdiği düz metin şifre.</param>
        /// <param name="hashString">Veritabanından alınan BCrypt formatındaki Hash.</param>
        /// <returns>Şifre doğruysa true, yanlışsa false.</returns>
        public bool VerifyPassword(string password, string hashString)
        {
            // BCrypt'in Verify metodu, kayıtlı hash string'ini ayrıştırır, 
            // içindeki tuzu ve maliyeti alarak gelen şifreyi hashler ve sonuçları karşılaştırır.
            return BCrypt.Net.BCrypt.Verify(password, hashString);
        }
    }
}